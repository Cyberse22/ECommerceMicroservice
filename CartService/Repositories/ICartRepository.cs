using CartService.Models;

namespace CartService.Repositories
{
    public interface ICartRepository
    {
        Task<CartModel> GetCartAsync(string userId);
        Task<bool> UpdateCartAsync(CartModel model);
        Task<bool> DeleteCartAsync(string userId);
    }
}
