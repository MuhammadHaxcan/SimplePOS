using System.ComponentModel.DataAnnotations;

namespace SimplePOS.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Customer is required.")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public ICollection<OrderItem>? Items { get; set; }

        [Range(0, 100000000, ErrorMessage = "Total must be zero or positive.")]
        public decimal Total { get; set; }
    }

}
