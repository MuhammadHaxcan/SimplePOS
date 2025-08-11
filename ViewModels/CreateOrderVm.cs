namespace SimplePOS.ViewModels
{
    public class CreateOrderVm
    {
        // if CustomerId == 0 => create on the fly  
        public int? CustomerId { get; set; }
        public string? NewCustomerName { get; set; }
        public string? NewCustomerContact { get; set; }

        public List<OrderItemVm> Items { get; set; } = new();
    }

}
