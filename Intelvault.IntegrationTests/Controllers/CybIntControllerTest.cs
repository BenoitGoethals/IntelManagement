using System.Text;
using IntelVault.ApplicationCore.IntelData;
using IntelVault.ApplicationCore.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace IntelVault.IntegrationTests.Controllers;
[Trait("Category", "Integration")]
public class CybIntControllerTest : IClassFixture<WebApplicationFactory<StartupTestWeb>>
{

    private readonly WebApplicationFactory<IntelVault.IntelApi.Program> _factory;

    public CybIntControllerTest(WebApplicationFactory<IntelVault.IntelApi.Program> factory)
    {
        this._factory = factory;
    }
   

    //[Theory]
    //[InlineData("/")]
    //[InlineData("/CybInt")]
    //[InlineData("/Humint")]
    public async Task Get_ReturnsSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        //response.EnsureSuccessStatusCode();
    }

    //[Fact]
    public async Task Post_ReturnsSuccessAndCreatedCybInt()
    {
        // Arrange
        var client = _factory.CreateClient();
        var cyBInt = GetCyInt();
        var content = new StringContent(JsonConvert.SerializeObject(cyBInt), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PostAsync("/CybInt", content);
        response.EnsureSuccessStatusCode();
        var createdcyBInto = JsonConvert.DeserializeObject<CybInt>(await response.Content.ReadAsStringAsync());

        // Assert
      
        Assert.Equal("Test Todo", createdcyBInto?.Attribution);
    }

    //[Fact]
    public async Task Put_ReturnsSuccessAndUpdatedTodo()
    {
        // Arrange
        var client = _factory.CreateClient();
        var updatedTodo = GetCyInt();
        var content = new StringContent(JsonConvert.SerializeObject(updatedTodo), Encoding.UTF8, "application/json");

        // Act
        var response = await client.PutAsync("/api/todo/1", content);

        // Assert
        response.EnsureSuccessStatusCode();
        // Additional assertions can be added to ensure the todo was actually updated
    }

    //[Fact]
    public async Task Delete_ReturnsSuccess()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.DeleteAsync("/api/todo/1");

        // Assert
        response.EnsureSuccessStatusCode();
        // Additional asserti

    }
    private CybInt GetCyInt()
    {
        return new CybInt()
        {
            ImpactAssessment = "dsfdsfdsf",
            Attribution = "sdfdsfdsf",
            IncidentDescription = "Sdfsdfdsf",
            IncidentType = TypeOfCybInt.DataBreaches,
            MalwareAnalysis = "sdfdsfdsf",
            Timeline = new List<ListItemDate>() {new ListItemDate(),new ListItemDate() },
        };
    }
}