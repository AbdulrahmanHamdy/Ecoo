using ECOO.Application.ViewModels;

namespace ECOO.Application.Interfaces.Services;


public interface ICartService
{
    
    CartViewModel GetCart();

  
    void AddToCart(int productId, string name, decimal price, string imageUrl);

  
    void UpdateQuantity(int productId, int quantity);

  
    void RemoveFromCart(int productId);

   
    void ClearCart();

    
    int GetCartItemCount();
}
