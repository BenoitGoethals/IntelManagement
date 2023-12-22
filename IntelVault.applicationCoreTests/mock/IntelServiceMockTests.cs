using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.ApplicationCore.validation;
using Moq;
using Xunit;
using IntelVault.Infrastructure.repos;
using System.Threading;

namespace IntelVault.applicationCoreTests.mock;

public class IntelServiceMockTests
{
   
    public IntelServiceMockTests()
    {
        
    }
    [Trait("Category", "Tests")]
    [Fact]
    public async Task MyService_GetAllAsync_ShouldReturnData()
    {
        // Arrange
        var repositoryMock = new Mock<IMongoDbRepository<HumInt>>();
        repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<HumInt> { /* your sample data here */ });

        var myService = new IntelService<HumInt>(repositoryMock.Object, validator: new HumIntValidator()!);

        // Act
        var result = await myService.GetAll();

        // Assert
        repositoryMock.Verify(s => s.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(result);
    }

    [Fact()]
    [Trait("Category", "Tests")]
    public async void UpdateTest()
    {
        // Arrange
        HumInt? humInt = GetHumit();
        var repositoryMock = new Mock<IMongoDbRepository<HumInt>>();
        repositoryMock.Setup(repo => repo.UpdateAsync(humInt.Id, humInt, It.IsAny<CancellationToken>()));

        var myService = new IntelService<HumInt>(repositoryMock.Object, validator: new HumIntValidator()!);
        humInt.ContactName = "fdsfds";

        // Act
        await myService.Update(humInt);

        // Assert
        repositoryMock.Verify(s => s.UpdateAsync(humInt.Id, humInt, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact()]
    [Trait("Category", "Tests")]
    public async void DeleteTest()
    {
        // Arrange
        HumInt? humInt = GetHumit();
        var repositoryMock = new Mock<IMongoDbRepository<HumInt>>();
        repositoryMock.Setup(repo => repo.DeleteAsync(humInt.Id, It.IsAny<CancellationToken>()));

        var myService = new IntelService<HumInt>(repositoryMock.Object, validator: new HumIntValidator()!);

        // Act
        await myService.Delete(humInt);

        // Assert
        repositoryMock.Verify(s => s.DeleteAsync(humInt.Id, It.IsAny<CancellationToken>()), Times.Once);

    }


    [Fact()]
    [Trait("Category", "Tests")]
    public async Task DeleteAllTest()
    {
        // Arrange
        var repositoryMock = new Mock<IMongoDbRepository<HumInt>>();
        repositoryMock.Setup(repo => repo.DeleteAllAsync(It.IsAny<CancellationToken>()));

        var myService = new IntelService<HumInt>(repositoryMock.Object, validator: new HumIntValidator()!);

        // Act
        await myService.DeleteAll();

        // Assert
        repositoryMock.Verify(s => s.DeleteAllAsync(It.IsAny<CancellationToken>()), Times.Once);
    }


    [Fact()]
    [Trait("Category", "Tests")]
    public async void GetAddTest()
    {
        // Arrange
        HumInt? humInt = GetHumit();
        var repositoryMock = new Mock<IMongoDbRepository<HumInt>>();
        repositoryMock.Setup(repo => repo.InsertAsync(humInt, It.IsAny<CancellationToken>()));

        var myService = new IntelService<HumInt>(repositoryMock.Object, validator: new HumIntValidator()!);

        // Act
        await myService.Add(humInt);
        // Assert
        repositoryMock.Verify(s => s.InsertAsync(humInt, It.IsAny<CancellationToken>()), Times.Exactly(1));






    }


    private HumInt GetHumit()
    {
        return new HumInt()
        {
            Id = ObjectId.GenerateNewId(),
            AccessLevel = "test",
            AccuracyOfDetails = "sdfds",
            AssessmentAndAnalysis = "sfdsfds",
            ClassificationHandlingInstructions = "dsfdsf",
            ContactEmail = "sdfdsfds@fdfs.be",
            ContactName = "fsdfsd",
            ContactPhoneNumber = "dsfdsf",
            ContactTitle = "John Doe",
            ContextBackground = "sdfsdf",
            IntelligenceDetails = new List<ListItem>(),
            LastContactDate = DateTime.Now,
            OperationalStatus = "asdsa",
            ReliabilityCredibility = "fdsds",
            ReliabilityRating = 100,
            SourceAffiliation = "dsad",
            SourceInformation = "sdfdsf",
            SourceName = "sdfdsf",
            SourceType = "sadsa",
            TimeLocation = "uk"
        };
    }
}
