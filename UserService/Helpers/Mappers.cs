using AutoMapper;
using UserService.Data;
using UserService.Models;
namespace UserService.Helpers
{
    public class Mappers : Profile
    {
        public Mappers() 
        {
            CreateMap<PasswordModel, SignInModel>().ReverseMap();

        }    
    }
}
