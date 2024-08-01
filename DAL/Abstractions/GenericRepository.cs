namespace DAL.Abstractions;

public class GenericRepository<T> : IRepository<T> where T : class, new()
{
    public Task Add(T entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(T entity)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<T>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task Update(T entity)
    {
        throw new NotImplementedException();
    }
}
