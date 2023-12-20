namespace IntelVault.ApplicationCore.Interfaces;

public interface IIntelService<T>
{
    public Task Add(T entity);
    public Task Update(T entity);
    public Task Delete(T entity);
    public Task DeleteAll();
    public Task<IEnumerable<T?>> GetAll();
    Task<long> Count();

    public Task<IEnumerable<T?>> GetAll(int page, int pageSize);
}