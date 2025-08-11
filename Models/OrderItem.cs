using System.ComponentModel.DataAnnotations;

namespace SimplePOS.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Order is required.")]
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        [Required(ErrorMessage = "Product is required.")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 1000000, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit price is required.")]
        [Range(0.01, 1000000, ErrorMessage = "Unit price must be greater than zero.")]
        public decimal UnitPrice { get; set; }

        public decimal LineTotal => UnitPrice * Quantity;
    }
}
