using IntelVault.ApplicationCore.Interfaces;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.ApplicationCore.validation;

namespace IntelVault.IntegrationTests;

[Trait("Category", "Integration")]
public class DocumentServiceTests : MongoDbTestBase<IntelDocument>
{

    public DocumentServiceTests()
    {
        _service = new DocumentService(DbRepository, validator: new IntelDocumentValidator());
    }

    private readonly IDocumentService _service;

    [Trait("Category", "Integration")]
    [Fact()]
    public async void AddTest()
    {
        await _service.Store(Read("Pensioenen.pdf"), "test",DocumentType.PDF);
      
        DbRepository.GetAllAsync()?.Result?.FirstOrDefault().Should().BeOfType<IntelDocument>();

    }

    
}