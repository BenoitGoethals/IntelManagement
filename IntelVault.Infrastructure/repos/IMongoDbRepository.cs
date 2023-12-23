using System.Linq.Expressions;
using MongoDB.Bson;

namespace IntelVault.Infrastructure.repos;

public interface IMongoDbRepository<T> where T : MongoEntity?
{
    Task<IEnumerable<T?>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T?>> GetAll(int page, int pageSize, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(ObjectId id, CancellationToken cancellationToken = default);

    Task<long> Count();
    Task<IEnumerable<T>?> FindAsync(Expression<Func<T?, bool>> filter, CancellationToken cancellationToken = default);
    Task InsertAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateAsync(ObjectId id, T entity, CancellationToken cancellationToken = default);
    Task DeleteAsync(ObjectId id, CancellationToken cancellationToken = default);

    Task DeleteAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T?>> GetAll(int page, int pageSize, string field,string searchText, CancellationToken cancellationToken = default);
}