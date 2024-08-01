using DAL.Abstractions;
using DAL.Context;
using DAL.Models;

namespace DAL.Repositories;

public class ProductRepository : GenericRepository<Product>
{
    public ProductRepository(AppDbContext context) : base(context)
    {
    }
}
