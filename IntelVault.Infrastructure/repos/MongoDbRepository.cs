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

    public async Task<IEnumerable<T?>> GetAllAsync()
    {
        try
        {
            IAsyncCursor<T?> cursor = await _collection.FindAsync(FilterDefinition<T>.Empty);
            return await cursor.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task<IEnumerable<T?>> GetAll(int page, int pageSize)
    {
        try
        {
            IAsyncCursor<T?> cursor = await _collection.Find(FilterDefinition<T>.Empty).Skip(page)
                .Limit(pageSize)
                .ToCursorAsync(); ;
            return await cursor.ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task<T> GetByIdAsync(ObjectId id)
    {
        try
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            return await _collection!.Find(filter).FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task<IEnumerable<T>?> FindAsync(Expression<Func<T?, bool>> filter)
    {
        try
        {
            return await _collection.Find(filter!).ToListAsync();
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task InsertAsync(T? entity)
    {
        try
        {
            if (entity != null) await _collection.InsertOneAsync(entity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task UpdateAsync(ObjectId id, T? entity)
    {
        try
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            if (entity != null) await _collection.ReplaceOneAsync(filter!, entity);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }

    public async Task DeleteAsync(ObjectId id)
    {
        try
        {
            FilterDefinition<T> filter = Builders<T>.Filter.Eq("_id", id);
            await _collection.DeleteOneAsync(filter!);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }



    public async Task DeleteAllAsync()
    {
        try
        {
            await _database.DropCollectionAsync(typeof(T).Name);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            throw new VaultException(e.Message);
        }
    }
}