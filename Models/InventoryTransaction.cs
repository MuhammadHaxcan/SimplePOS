namespace SimplePOS.Models
{
    public enum InventoryTransactionType
    {
        Sale = 0,
        Purchase = 1,
        Adjustment = 2
    }

    public class InventoryTransaction
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public InventoryTransactionType Type { get; set; }

        // positive or negative quantity change (for sale it will be negative)
        public int QuantityChange { get; set; }

        public int? OrderId { get; set; }
    }

}
