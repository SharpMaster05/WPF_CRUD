namespace DAL.Abstractions;

public interface IRepository<T> where T : class, new()
{
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<IEnumerable<T>> GetAll();
}
