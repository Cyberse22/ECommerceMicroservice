using CartService.Models;

namespace CartService.Services
{
    public interface ICartService
    {
        Task<bool> AddItemToCartAsync(string UserId, CartItemModel item);
        Task<bool> ClearCartAsync(string userId);
        Task<CartModel> GetCartAsync(string UserId);
        Task<bool> RemoveItemFromCartAsync(string userId, string productId);
    }
}