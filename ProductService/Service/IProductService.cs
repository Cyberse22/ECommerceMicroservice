using ProductService.Data;
using ProductService.Models;

namespace ProductService.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(string productId);
        Task<Product> AddProductAsync(ProductModel product);
        Task UpdateProductAsync(string productId, ProductModel model);
        Task DeleteProductAsync(string productId);
    }
}
