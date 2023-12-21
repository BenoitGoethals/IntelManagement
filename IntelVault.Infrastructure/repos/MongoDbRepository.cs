using System.Runtime.CompilerServices;

namespace IntelVault.Infrastructure.repos;

using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System.Collections;
using System.Linq.Expressions;

public class MongoDbRepository<T> : IMongoDbRepository<T> where T : MongoEntity
{
    private readonly IMongoCollection<T> _collection;
    private readonly ILogger<IMongoDbRepository<T>> _logger;
    private readonly IMongoDatabase _database;


    public MongoDbRepository(IMongoClient client, ILogger<IMongoDbRepository<T>> logger, string databaseName)
    {
        _database = client.GetDatabase(databaseName);
        _collection = _database.GetCollection<T>(typeof(T).Name);
        _logger = logger;
    }

    public async Task<IEnumerable<T?>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            IAsyncCursor<T?> cursor = await _collection.FindAsync(FilterDefinition<T>.Empty);
            return await cursor.ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task<IEnumerable<T?>> GetAll(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        try
        {
            IAsyncCursor<T?> cursor = await _collection.Find(FilterDefinition<T>.Empty).Skip(page)
                .Limit(pageSize)
                .ToCursorAsync(); ;
            return await cursor.ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task<T> GetByIdAsync(ObjectId id, CancellationToken cancellationToken = default)
    {
        try
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            return await _collection!.Find(filter).FirstOrDefaultAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task<long> Count()
    {
        try
        {
            return await _collection.CountDocumentsAsync(new BsonDocument());
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task<IEnumerable<T>?> FindAsync(Expression<Func<T?, bool>> filter, CancellationToken cancellationToken = default)
    {
        try
        {
            return await _collection.Find(filter!).ToListAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task InsertAsync(T? entity, CancellationToken cancellationToken = default)
    {
        try
        {
            if (entity != null) await _collection.InsertOneAsync(entity,null,cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task UpdateAsync(ObjectId id, T? entity, CancellationToken cancellationToken = default)
    {
        try
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            if (entity != null) await _collection.ReplaceOneAsync(filter, entity, cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task DeleteAsync(ObjectId id, CancellationToken cancellationToken = default)
    {
        try
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            await _collection.DeleteOneAsync(filter!, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }



    public async Task DeleteAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _database.DropCollectionAsync(typeof(T).Name, cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }
}