using ProductService.Data;
using ProductService.Models;
using AutoMapper;

namespace ProductService.Helpers
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>()
                .ForMember(dest => dest.Id, otp => otp.Ignore());
            CreateMap<UpdateProductModel, Product>();
        }
    }
}
