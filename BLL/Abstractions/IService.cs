namespace BLL.Abstractions;

public interface IService<T> where T : class, new()
{
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(T entity);
    Task<IEnumerable<T>> GetAll();
    Task<T> GetById(int id);
}
