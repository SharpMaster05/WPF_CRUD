using AutoMapper;
using BLL.Abstractions;
using BLL.Dto;
using DAL.Abstractions;
using DAL.Models;

namespace BLL.Services;

public class ProductService : GenericService<ProductDto, Product>
{
    public ProductService(IRepository<Product> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}
