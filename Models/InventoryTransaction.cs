using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "Product is required.")]
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Transaction type is required.")]
        public InventoryTransactionType Type { get; set; }

        [Range(-100, 100, ErrorMessage = "Quantity change must be between -100 and 100.")]
        public int QuantityChange { get; set; }

        public int? OrderId { get; set; }
    }
}
