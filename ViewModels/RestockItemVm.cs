using System.ComponentModel.DataAnnotations;

namespace SimplePOS.ViewModels
{
    public class RestockItemVm
    {
        [Required(ErrorMessage = "Product is required.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100.")]
        public int Quantity { get; set; }
    }
}