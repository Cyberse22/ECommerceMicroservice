using AutoMapper;
using ProductService.Data;
using ProductService.Models;
using ProductService.Repository;

namespace ProductService.Service.Impl
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductServiceImpl(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<Product> AddProductAsync(ProductModel model)
        {
            var product = _mapper.Map<Product>(model);
            return await _productRepository.AddProduct(product);
        }

        public async Task DeleteProductAsync(string productId)
        { 
            await _productRepository.DeleteProductAsync(productId);
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync() => await _productRepository.GetAllProductsAsync();

        public async Task<Product> GetProductByIdAsync(string productId) => await _productRepository.GetProductByIdAsync(productId);

        public async Task UpdateProductAsync(string productId, ProductModel model)
        {
            var product = _mapper.Map<Product>(model);
            await _productRepository.UpdateProductAsync(productId, product);
        }
    }
}
