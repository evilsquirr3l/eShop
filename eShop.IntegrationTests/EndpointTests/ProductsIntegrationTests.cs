using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Business.Models;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace eShop.IntegrationTests.EndpointTests
{
    [TestFixture]
    public class ProductsIntegrationTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private const string RequestUri = "api/products/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task ProductsController_GetAll_ReturnsProducts()
        {
            var httpResponse = await _client.GetAsync(RequestUri);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<ProductDto>>(stringResponse);

            products.Count().Should().Be(2);
        }

        [Test]
        public async Task ProductsController_GetById_ReturnsProductDto()
        {
            var httpResponse = await _client.GetAsync(RequestUri + 1);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDto>(stringResponse);

            product.Id.Should().Be(1);
            product.Name.Should().Be("Vi");
        }
    }
}
