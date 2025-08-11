using SimplePOS.Models;
using SimplePOS.ViewModels;

namespace SimplePOS.Services
{
    public interface IPosService
    {
        Task<Order> CreateOrderAsync(CreateOrderVm vm, CancellationToken ct = default);
        Task RestockAsync(RestockItemVm vm, CancellationToken ct = default);
    }

}
