using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntelVault.ApplicationCore.Model;
using IntelVault.ApplicationCore.Services;
using IntelVault.ApplicationCore.validation;
using Moq;
using IntelVault.IntelApi.Controllers;
using Microsoft.AspNetCore.Components.Server;
using IntelVault.Infrastructure.repos;

namespace IntelVault.applicationTests.Controllers
{
    public class CybIntControllerTests
    {
        [Fact()]
        public async Task CybIntControllerTest()
        {

            // Arrange
            var repositoryMock = new Mock<IMongoDbRepository<CybInt>>();
            repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(new List<CybInt> { /* your sample data here */ });

            var myService = new IntelService<CybInt>(repositoryMock.Object, validator: new CybIntValidator());
            CybIntController controller = new CybIntController(myService);
            // Act
            var result = await myService.GetAll();

            // Assert
            repositoryMock.Verify(s => s.GetAllAsync(), Times.Once);
        }
    }
}

//https://www.hosting.work/aspnet-core-web-api-xunit-moq-unit-testing/?utm_content=cmp-true