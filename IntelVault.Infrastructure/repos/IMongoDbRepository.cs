using System.Linq.Expressions;
using MongoDB.Bson;

namespace IntelVault.Infrastructure.repos;

public interface IMongoDbRepository<T> where T : BaseIntel?
{
    Task<IEnumerable<T?>> GetAllAsync();
    Task<T> GetByIdAsync(ObjectId id);
    Task<IEnumerable<T>?> FindAsync(Expression<Func<T?, bool>> filter);
    Task InsertAsync(T entity);
    Task UpdateAsync(ObjectId id, T entity);
    Task DeleteAsync(ObjectId id);

    Task DeleteAllAsync();
}