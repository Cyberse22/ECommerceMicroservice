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
            var updateDefinition = Builders<Product>.Update
                .Set(p => p.ProductName, product.ProductName)
                .Set(p => p.ProductDescription, product.ProductDescription)
                .Set(p => p.ProductPrice, product.ProductPrice)
                .Set(p => p.IsActive, product.IsActive);
            // Các trường cần cập nhật khác nếu cần

            var result = await _products.UpdateOneAsync(p => p.ProductId == productId, updateDefinition);

            // Kiểm tra nếu không tìm thấy sản phẩm để cập nhật
            if (result.MatchedCount == 0)
            {
                throw new Exception("Product not found");
            }
        }
    }
}
