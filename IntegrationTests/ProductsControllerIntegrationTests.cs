using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Business.Records;
using FluentAssertions;
using NUnit.Framework;
using WebApi;

namespace IntegrationTests;

[TestFixture]
public class ProductsControllerIntegrationTests
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
    public async Task GetProduct_WhenProductDoesntExist_ReturnsNotFound()
    {
        var response = await _client.GetAsync($"api/Products/{999}");

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
    
    [Test]
    public async Task GetProduct_WhenProductExists_ReturnsProduct()
    {
        var response = await _client.GetAsync($"api/Products/{1}");

        var result = await response.Content.ReadFromJsonAsync<ProductRecord>();
        response.EnsureSuccessStatusCode();
        result.Should().NotBeNull();
    }
    
    [Test]
    public async Task CreateProduct_WithInvalidProduct_ReturnsBadRequest()
    {
        var productRecord = new ProductRecord {Name = "invalid product"};
        
        var response = await _client.PostAsJsonAsync<ProductRecord>("api/Products", productRecord);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Test]
    public async Task CreateProduct_WithInvalidCategory_ReturnsBadRequest()
    {
        var productRecord = new ProductRecord {Name = "product", Description = "description", CategoryId = 999};
        
        var response = await _client.PostAsJsonAsync<ProductRecord>("api/Products", productRecord);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Test]
    public async Task CreateProduct_WithValidProduct_ReturnsCreatedAtAction()
    {
        var productRecord = new ProductRecord {Name = "valid product", Description = "description", CategoryId = 1};
        
        var response = await _client.PostAsJsonAsync<ProductRecord>("api/Products", productRecord);

        var result = await response.Content.ReadFromJsonAsync<ProductRecord>();
        response.EnsureSuccessStatusCode();
        result.Should().NotBeNull();
    }
    
    [Test]
    public async Task UpdateExistingProduct_WithValidProduct_ReturnsNoContent()
    {
        var id = 1;
        var productRecord = new ProductRecord {Id = id, Name = "updated product", Description = "updated description", CategoryId = 1};

        var response = await _client.PutAsJsonAsync($"api/Products/{id}", productRecord);

        response.EnsureSuccessStatusCode();
    }
    
    [Test]
    public async Task DeleteExistingProduct_ReturnsNoContent()
    {
        var response = await _client.DeleteAsync($"api/Products/{2}");

        response.EnsureSuccessStatusCode();
    }

    [TearDown]
    public void TearDown()
    {
        _testWebAppFactory.Dispose();
        _client.Dispose();
    }
}