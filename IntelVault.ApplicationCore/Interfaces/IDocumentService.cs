using IntelVault.ApplicationCore.Model;

namespace IntelVault.ApplicationCore.Interfaces;

public interface IDocumentService
{
    Task Store(string document, string description);
    public Task Store(IntelDocument? intelDocumentation);
    Task<IntelDocument?> Store(byte[]? content, string description, DocumentType documentType);
    Task Add(IntelDocument entity);
    Task Update(IntelDocument entity);
    Task Delete(IntelDocument entity);
    Task DeleteAll();
    Task<IEnumerable<IntelDocument?>> GetAll();
    public Task<IEnumerable<IntelDocument?>> GetAll(int page, int pageSize);

    Task<long> Count();
}