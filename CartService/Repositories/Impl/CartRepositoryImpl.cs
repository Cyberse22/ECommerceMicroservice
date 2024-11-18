using CartService.Models;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CartService.Repositories.Impl
{
    public class CartRepositoryImpl : ICartRepository
    {
        private readonly IDistributedCache _redisCache;
        public CartRepositoryImpl(IDistributedCache redisCache) 
        {
            _redisCache = redisCache;
        }

        public async Task<bool> DeleteCartAsync(string userId)
        {
            await _redisCache.RemoveAsync(userId);
            return true;
        }

        public async Task<CartModel> GetCartAsync(string userId)
        {
            var cartData = await _redisCache.GetStringAsync(userId);
            return string.IsNullOrEmpty(cartData) ? null : JsonConvert.DeserializeObject<CartModel>(cartData);
        }

        public async Task<bool> UpdateCartAsync(CartModel model)
        {
            var serializedCart = JsonConvert.SerializeObject(model);
            await _redisCache.SetStringAsync(model.UserId, serializedCart);
            return true;
        }
    }
}
