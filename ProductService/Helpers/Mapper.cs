using ProductService.Data;
using ProductService.Models;
using AutoMapper;

namespace ProductService.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>()
                .ForMember(dest => dest.Id, otp => otp.Ignore());
        }
    }
}
