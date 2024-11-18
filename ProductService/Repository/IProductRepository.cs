using ProductService.Data;

namespace ProductService.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(string productId);
        Task<Product> AddProduct(Product product);
        Task UpdateProductAsync(string productId, Product product);
        Task DeleteProductAsync(string productId);
    }
}
