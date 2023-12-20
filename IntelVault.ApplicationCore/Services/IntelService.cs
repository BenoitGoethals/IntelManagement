using FluentValidation;
using IntelVault.ApplicationCore.Constants;
using IntelVault.ApplicationCore.IntelData;
using IntelVault.ApplicationCore.Interfaces;
using IntelVault.Infrastructure.repos;

namespace IntelVault.ApplicationCore.Services;

public class IntelService<T>(IMongoDbRepository<T> mongodbDbRepository, AbstractValidator<T> validator)
    : IIntelService<T>
    where T : BaseIntel
{


    private readonly IValidator<T> _validator = validator;

    public async Task Add(T entity)
    {
        var validationResult = await _validator.ValidateAsync(entity);
        if (validationResult.IsValid)
        {
            entity.IntelVaultUser = await GetUserName();
            await mongodbDbRepository.InsertAsync(entity);
        }
    }

    public async Task Update(T entity)
    {
        await mongodbDbRepository.UpdateAsync(entity.Id, entity);
    }

    public async Task Delete(T entity)
    {
        await mongodbDbRepository.DeleteAsync(entity.Id);
    }

    public async Task DeleteAll()
    {
        await mongodbDbRepository.DeleteAllAsync();
    }

    public async Task<IEnumerable<T?>> GetAll(int page, int pageSize)
    {
        return await mongodbDbRepository.GetAll(page,pageSize);
    }

    public async Task<IEnumerable<T?>> GetAll()
    {
        return await mongodbDbRepository.GetAllAsync();
    }

    public Task<string?> GetUserName()
    {

        return Task.FromResult(GlobalUser.UserName); ;
    }
}