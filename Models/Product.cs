using System.ComponentModel.DataAnnotations;
namespace SimplePOS.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [MaxLength(50, ErrorMessage = "Product name cannot exceed 50 characters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, 1000000, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity in stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Quantity in stock cannot be negative.")]
        public int QuantityInStock { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }
        public ICollection<InventoryTransaction>? InventoryTransactions { get; set; }
    }

}
