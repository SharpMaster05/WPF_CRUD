using DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace DAL.Abstractions;

public class GenericRepository<T> : IRepository<T> where T : class, new()
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context) 
    {
        _context = context;
    }

    public async Task Add(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
    
    public async Task Update(T entity)
    {
        _context.Set<T>().Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();
    public async Task<T> GetById(int id) => await _context.Set<T>().FindAsync(id);
}
