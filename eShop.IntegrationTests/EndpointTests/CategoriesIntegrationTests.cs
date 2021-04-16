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
    public class CategoriesIntegrationTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private const string RequestUri = "api/categories/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task CategoriesController_GetAll_ReturnsCategories()
        {
            var httpResponse = await _client.GetAsync(RequestUri);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(stringResponse);

            categories.Count().Should().Be(2);
        }

        [Test]
        public async Task CategoriesController_GetById_ReturnsCategoryDto()
        {
            var httpResponse = await _client.GetAsync(RequestUri + 1);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var categories = JsonConvert.DeserializeObject<CategoryDto>(stringResponse);

            categories.Id.Should().Be(1);
            categories.Name.Should().Be("Pa");
        }
    }
}
