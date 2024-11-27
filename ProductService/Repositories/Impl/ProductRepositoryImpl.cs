using MongoDB.Driver;
using ProductService.Data.MongoDB;
using ProductService.Data;

namespace ProductService.Repositories.Impl
{
    public class ProductRepositoryImpl : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepositoryImpl(MongoDbServices? mongoDbService)
        {
            _products = mongoDbService!.Database!.GetCollection<Product>("productcollection");
        }

        public async Task<Product> AddProduct(Product product)
        {
            await _products.InsertOneAsync(product);
            return product;
        }

        public async Task DeleteProductAsync(string productId)
        {
            await _products.DeleteOneAsync(p => p.ProductId == productId);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _products.Find(_ => true).ToListAsync();

        public async Task<Product> GetProductByIdAsync(string productId) => await _products.Find(p => p.ProductId == productId).FirstOrDefaultAsync();

        public async Task UpdateProductAsync(string productId, Product product)
        {
            await _products.ReplaceOneAsync(p => p.ProductId == productId, product);
        }
    }
}
