using AutoMapper;
using ProductManagement.App.ViewModels;
using ProductManagement.Business.Models;

namespace ProductManagement.App.Automapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
        }
    }
}
