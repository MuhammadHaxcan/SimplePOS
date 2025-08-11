using System.ComponentModel.DataAnnotations;

namespace SimplePOS.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [MaxLength(50, ErrorMessage = "Customer name cannot exceed 50 characters.")]
        public string Name { get; set; } = null!;

        [MaxLength(50, ErrorMessage = "Contact number cannot exceed 50 characters.")]
        [RegularExpression(@"^[\d\-\+\s\(\)]*$", ErrorMessage = "Contact number contains invalid characters.")]
        public string? ContactNumber { get; set; }

        public ICollection<Order>? Orders { get; set; }
    }
}
