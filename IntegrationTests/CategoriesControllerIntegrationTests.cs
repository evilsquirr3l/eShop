using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Business.Records;
using FluentAssertions;
using NUnit.Framework;

namespace IntegrationTests;

[TestFixture]
public class CategoriesControllerIntegrationTests
{
    private TestWebAppFactory<Program> _testWebAppFactory;
    private HttpClient _client;
    
    [SetUp]
    public void Setup()
    {
        _testWebAppFactory = new TestWebAppFactory<Program>();
        _client = _testWebAppFactory.CreateClient();
    }

    [Test]
    public async Task GetCategory_WhenCategoryDoesntExist_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"api/Categories/{999}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Test]
    public async Task GetCategory_WhenCategoryExists_ReturnsCategory()
    {
        var response = await _client.GetAsync($"api/Categories/{1}");

        var result = await response.Content.ReadFromJsonAsync<CategoryRecord>();
        response.EnsureSuccessStatusCode();
        result.Should().NotBeNull();
    }
    
    [Test]
    public async Task CreateCategory_WhenCategoryWithTheSameNameExists_ReturnsBadRequest()
    {
        var categoryRecord = new CategoryRecord {Name = "category1", Description = "category1 description"};
        
        var response = await _client.PostAsJsonAsync("api/Categories", categoryRecord);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Test]
    public async Task CreateCategory_WithValidCategory_ReturnsCreatedAtAction()
    {
        var categoryRecord = new CategoryRecord {Name = "valid category", Description = "description"};
        
        var response = await _client.PostAsJsonAsync("api/Categories", categoryRecord);

        var result = await response.Content.ReadFromJsonAsync<CategoryRecord>();
        response.EnsureSuccessStatusCode();
        result.Should().NotBeNull();
    }
    
    [Test]
    public async Task UpdateExistingCategory_WithValidCategory_ReturnsNoContent()
    {
        var id = 1;
        var categoryRecord = new CategoryRecord {Id = id, Name = "updated category", Description = "updated description"};

        var response = await _client.PutAsJsonAsync($"api/Categories/{id}", categoryRecord);

        response.EnsureSuccessStatusCode();
    }
    
    [Test]
    public async Task DeleteExistingCategory_ReturnsNoContent()
    {
        var response = await _client.DeleteAsync($"api/Categories/{2}");

        response.EnsureSuccessStatusCode();
    }

    [TearDown]
    public void TearDown()
    {
        _testWebAppFactory.Dispose();
        _client.Dispose();
    }
}