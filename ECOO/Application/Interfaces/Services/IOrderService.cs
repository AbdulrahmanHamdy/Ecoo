using ECOO.Application.ViewModels;

namespace ECOO.Application.Interfaces.Services;


public interface IOrderService
{
    
    Task<int> PlaceOrderAsync(CheckoutViewModel model);

    Task<OrderViewModel?> GetOrderByIdAsync(int id);
    Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();
}
