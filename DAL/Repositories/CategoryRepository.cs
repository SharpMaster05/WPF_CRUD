using DAL.Abstractions;
using DAL.Context;
using DAL.Models;

namespace DAL.Repositories;

public class CategoryRepository : GenericRepository<Category>
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }
}
