using System.ComponentModel.DataAnnotations;

namespace SimplePOS.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public ICollection<OrderItem>? Items { get; set; }

        public decimal Total { get; set; }
    }

}
