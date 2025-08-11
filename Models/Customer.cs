using System.ComponentModel.DataAnnotations;

namespace SimplePOS.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string? ContactNumber { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }

}
