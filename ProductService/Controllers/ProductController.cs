using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using ProductService.Data;
using ProductService.Data.MongoDB;
using ProductService.Models;
using ProductService.Services;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync() => Ok(await _productService.GetAllProductsAsync());
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductByIdAsync(string productId) => Ok(await _productService.GetProductByIdAsync(productId));
        [HttpPost]
        public async Task<IActionResult> AddProductAsync(ProductModel model)
        {
            await _productService.AddProductAsync(model);
            return Ok(model);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProductAsync(string productId, ProductModel model)
        {
            await _productService.UpdateProductAsync(productId, model);
            return NoContent();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteProductAsync(string productId) 
        {
            await _productService.DeleteProductAsync(productId);
            return NoContent();
        }
    }
}
