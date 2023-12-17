﻿using IntelVault.ApplicationCore.IntelData;

namespace IntelVault.ApplicationCore.Services;

public interface IGlobalService
{
    public Task Add<T>(T entity) where T:BaseIntel;
    public Task Update<T>(T entity) where T : BaseIntel;
    public Task Delete<T>(T entity) where T : BaseIntel;
    public Task DeleteAll<T>();
    public Task<IEnumerable<T>> GetAll<T>() where T : BaseIntel;
    public Task<IEnumerable<BaseIntel>> GetAll();
}