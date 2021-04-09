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
    public class CartsIntegrationTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private const string RequestUri = "api/carts/";
        
        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Test]
        public async Task CartsController_GetAll_ReturnsCarts()
        {
            var httpResponse = await _client.GetAsync(RequestUri);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var carts = JsonConvert.DeserializeObject<IEnumerable<CartDto>>(stringResponse);

            carts.Count().Should().Be(2);
        }
        
        [Test]
        public async Task CartsController_GetById_ReturnsCartDto()
        {
            var httpResponse = await _client.GetAsync(RequestUri + 1);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var cart = JsonConvert.DeserializeObject<CartDto>(stringResponse);

            cart.Id.Should().Be(1);
            cart.TotalPrice.Should().Be(1488);
        }
    }
}