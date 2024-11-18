using CartService.Models;
using CartService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CartService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCart(string userId)
        {
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }
        [HttpPost("{userId}/items")]
        public async Task<IActionResult> AddItemToCart(string userId, CartItemModel item)
        {
            var result = await _cartService.AddItemToCartAsync(userId, item);
            return result ? Ok(result) : BadRequest();
        }
        [HttpDelete("{userId}/items/{productId}")]
        public async Task<IActionResult> RemoveItemToCart(string userId, string productId)
        {
            var result = await _cartService.RemoveItemFromCartAsync(userId, productId);
            return result ? Ok(result) : NotFound();
        }
        [HttpDelete("{userId}")]
        public async Task<IActionResult> ClearCart(string userId)
        {
            var result = await _cartService.ClearCartAsync(userId);
            return result ? Ok(result) : BadRequest();
        }
    }
}
