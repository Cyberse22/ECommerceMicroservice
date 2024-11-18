using CartService.Models;
using CartService.Repositories;

namespace CartService.Services.Impl
{
    public class CartServiceImpl : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartServiceImpl(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<bool> AddItemToCartAsync(string UserId, CartItemModel item)
        {
            var cart = await GetCartAsync(UserId);
            var existsItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
            if (existsItem != null)
            {
                existsItem.Quantity += item.Quantity;
            }
            else
            {
                cart.Items.Add(item);
            }
            return await _cartRepository.UpdateCartAsync(cart);
        }

        public async Task<bool> ClearCartAsync(string userId)
        {
            return await _cartRepository.DeleteCartAsync(userId);
        }

        public async Task<CartModel> GetCartAsync(string UserId)
        {
            return await _cartRepository.GetCartAsync(UserId) ?? new CartModel { UserId = UserId };
        }

        public async Task<bool> RemoveItemFromCartAsync(string userId, string productId)
        {
            var cart = await GetCartAsync(userId);
            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Items.Remove(item);
                return await _cartRepository.UpdateCartAsync(cart);
            }
            return false;
        }
    }
}
