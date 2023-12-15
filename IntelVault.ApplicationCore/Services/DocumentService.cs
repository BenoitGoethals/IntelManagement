using FluentValidation;
using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.Infrastructure.repos;


namespace IntelVault.ApplicationCore.Services;

public class DocumentService(IMongoDbRepository<IntelDocument> mongodbDbRepository,
    AbstractValidator<IntelDocument> validator) : IntelService<IntelDocument>(mongodbDbRepository: mongodbDbRepository, validator), IDocumentService
{
    private IReader? _reader;

    public async Task<IntelDocument?> Store(byte[]? content, string description, DocumentType documentType)
    {
        if (content?.Length > 0)
        {
            var user = await GetUserName();
            IntelDocument? intelDocumentation = new IntelDocument() { Content = content, Description = description, DocumentType = documentType, TimeCreated = DateTime.Now, IntelVaultUser = user };
            await mongodbDbRepository.InsertAsync(intelDocumentation);
            return intelDocumentation;
        }

        return null;
    }
    public async Task Store(string document, string description)
    {
        var fileInfo = new FileInfo(document);
        if (fileInfo.Exists)
        {
            var output = _reader?.GetBytes(document);
            if (output?.Length > 0)
            {
                var user = await GetUserName();
                IntelDocument? intelDocumentation = new IntelDocument() { Content = output, Description = description, DocumentType = AnalyseDocumentType(fileInfo.Extension.ToLower()), TimeCreated = DateTime.Now };
                await mongodbDbRepository.InsertAsync(intelDocumentation);
            }
        }
    }

    public async Task Store(IntelDocument? intelDocumentation)
    {
        var user = await GetUserName();
        if (intelDocumentation != null)
        {
            intelDocumentation.IntelVaultUser = user;
            await mongodbDbRepository.InsertAsync(intelDocumentation);
        }
    }

    private DocumentType AnalyseDocumentType(string extension)
    {
        switch (extension)
        {
            case "pdf":
                return DocumentType.PDF;
            case "docx":

                return DocumentType.Docx;
        }
        return DocumentType.Nothing;
    }
}