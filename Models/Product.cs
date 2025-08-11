using System.ComponentModel.DataAnnotations;
namespace SimplePOS.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<InventoryTransaction>? InventoryTransactions { get; set; }
    }

}
