using ProductService.Data;
using ProductService.Models;

namespace ProductService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(string productId);
        Task<Product> AddProductAsync(ProductModel product);
        Task DeleteProductAsync(string productId);
        Task UpdateProductAsync(string productId, UpdateProductModel model);
    }
}
