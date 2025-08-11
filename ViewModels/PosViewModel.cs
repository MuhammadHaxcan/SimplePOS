using SimplePOS.Models;

namespace SimplePOS.ViewModels
{
    public class PosViewModel
    {
        public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();
        public IEnumerable<Customer> Customers { get; set; } = Enumerable.Empty<Customer>();
    }

}
