namespace SimplePOS.Services
{
    using Microsoft.EntityFrameworkCore;
    using SimplePOS.Data;
    using SimplePOS.Models;
    using SimplePOS.ViewModels;

    public class PosService : IPosService
    {
        private readonly AppDbContext _db;

        public PosService(AppDbContext db) { _db = db; }

        public async Task<Order> CreateOrderAsync(CreateOrderVm vm, CancellationToken ct = default)
        {
            if (vm.Items == null || !vm.Items.Any())
                throw new ArgumentException("Order must contain at least one item.");

            using var tx = await _db.Database.BeginTransactionAsync(ct);

            // create or find customer -- on the fly approach.
            Customer customer;
            if (vm.CustomerId.HasValue && vm.CustomerId.Value > 0)
            {
                customer = await _db.Customers.FindAsync(new object[] { vm.CustomerId.Value }, ct)
                    ?? throw new InvalidOperationException("Customer not found.");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(vm.NewCustomerName))
                    throw new ArgumentException("New customer name is required when no CustomerId is provided.");

                customer = new Customer { Name = vm.NewCustomerName.Trim(), ContactNumber = vm.NewCustomerContact?.Trim() };
                _db.Customers.Add(customer);
                await _db.SaveChangesAsync(ct);
            }

            var productIds = vm.Items.Select(i => i.ProductId).Distinct().ToList();
            var products = await _db.Products.Where(p => productIds.Contains(p.Id)).ToDictionaryAsync(p => p.Id, ct);

            var order = new Order { CustomerId = customer.Id, CreatedAt = DateTime.UtcNow, Items = new List<OrderItem>() };

            decimal total = 0m;
            foreach (var itemVm in vm.Items)
            {
                if (!products.TryGetValue(itemVm.ProductId, out var prod))
                    throw new InvalidOperationException($"Product {itemVm.ProductId} not found.");

                if (itemVm.Quantity <= 0)
                    throw new InvalidOperationException("Quantity must be greater than zero.");

                if (prod.QuantityInStock < itemVm.Quantity)
                    throw new InvalidOperationException($"Not enough stock for {prod.Name}.");

                var oi = new OrderItem
                {
                    ProductId = prod.Id,
                    Quantity = itemVm.Quantity,
                    UnitPrice = prod.Price
                };
                order.Items.Add(oi);

                // update stock
                prod.QuantityInStock -= itemVm.Quantity;

                // create inventory transaction (sale => negative)
                var invTx = new InventoryTransaction
                {
                    ProductId = prod.Id,
                    QuantityChange = -itemVm.Quantity,
                    Type = InventoryTransactionType.Sale,
                    OrderId = order.Id 
                };
                _db.InventoryTransactions.Add(invTx);

                total += oi.LineTotal;
            }

            order.Total = total;
            _db.Orders.Add(order);

            await _db.SaveChangesAsync(ct);

            await tx.CommitAsync(ct);

            return order;
        }
    }

}
