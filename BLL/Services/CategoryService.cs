using AutoMapper;
using BLL.Abstractions;
using BLL.Dto;
using DAL.Abstractions;
using DAL.Models;

namespace BLL.Services;

public class CategoryService : GenericService<CategoryDto, Category>
{
    public CategoryService(IRepository<Category> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
