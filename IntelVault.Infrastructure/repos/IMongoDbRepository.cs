using System.Linq.Expressions;
using MongoDB.Bson;

namespace IntelVault.Infrastructure.repos;

public interface IMongoDbRepository<T> where T : MongoEntity?
{
    Task<IEnumerable<T?>> GetAllAsync();
    Task<IEnumerable<T?>> GetAll(int page, int pageSize);
    Task<T> GetByIdAsync(ObjectId id);

    Task<long> Count();
    Task<IEnumerable<T>?> FindAsync(Expression<Func<T?, bool>> filter);
    Task InsertAsync(T entity);
    Task UpdateAsync(ObjectId id, T entity);
    Task DeleteAsync(ObjectId id);

    Task DeleteAllAsync();
}