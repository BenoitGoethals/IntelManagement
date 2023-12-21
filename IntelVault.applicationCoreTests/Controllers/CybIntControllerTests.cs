using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.ApplicationCore.validation;
using IntelVault.Infrastructure.repos;
using Moq;
using IntelVault.IntelApi.Controllers;
using System.Threading;

namespace IntelVault.applicationCoreTests.Controllers
{
    public class CybIntControllerTests
    {
        [Fact()]
        public async Task CybIntControllerTest()
        {
          
         
            // Arrange
            var repositoryMock = new Mock<IMongoDbRepository<CybInt>>();
            repositoryMock.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>())).ReturnsAsync(new List<CybInt> { /* your sample data here */ });

            var myService = new IntelService<CybInt>(repositoryMock.Object, validator: new CybIntValidator()!);
            CybIntController controller = new CybIntController(myService);
            // Act
            var result = await myService.GetAll();

            // Assert
            repositoryMock.Verify(s => s.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

//https://www.hosting.work/aspnet-core-web-api-xunit-moq-unit-testing/?utm_content=cmp-true