using AutoMapper;
using BLL.Dto;
using DAL.Models;

namespace WPF_CRUD.Infrastucture;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}
