using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Business.Models;
using Data.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace eShop.IntegrationTests
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

        [Test]
        public async Task CategoriesController_Update_UpdatesCategory()
        {
            var updatedCategory = new CategoryDto {Id = 2, Products = new List<ProductDto>(), Name = "UpdatedCategory"};
            var payload = JsonConvert.SerializeObject(updatedCategory);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PutAsync(RequestUri + 2,content);

            var actualHttpResponse = await _client.GetAsync(RequestUri + 2);


            // Maybe just checking for the SuccessStatusCode will suffice?
            // Same with the other Create / Delete methods.
            httpResponse.EnsureSuccessStatusCode();
            var stringActualResponse = await actualHttpResponse.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<CategoryDto>(stringActualResponse);
            category.Id.Should().Be(2);
            category.Name.Should().Be("UpdatedCategory");
        }

        [Test]
        public async Task CategoriesController_Create_CreatesCategory()
        {
            var updatedCategory = new CategoryDto { Id = 3, Products = new List<ProductDto>(), Name = "newCategory" };
            var payload = JsonConvert.SerializeObject(updatedCategory);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var httpResponse = await _client.PostAsync(RequestUri, content);

            var actual = await _client.GetAsync(RequestUri + 3);

            httpResponse.EnsureSuccessStatusCode();
            var stringActualResponse = await actual.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<CategoryDto>(stringActualResponse);
            category.Id.Should().Be(3);
            category.Name.Should().Be("newCategory");
        }


        // Maybe just checking for the SuccessStatusCode will suffice?
        // Same with the other Create / Delete methods.
        [Test]
        public async Task CategoriesController_Delete_DeletesCategory()
        {
            var actualRequestBeforeDelete = await _client.GetAsync(RequestUri);
            actualRequestBeforeDelete.EnsureSuccessStatusCode();
            var stringBeforeDeleteResponse = await actualRequestBeforeDelete.Content.ReadAsStringAsync();
            var categoriesBeforeDelete = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(stringBeforeDeleteResponse);

            
            var httpResponse = await _client.DeleteAsync(RequestUri + 1);

            var actualRequestAfterDelete = await _client.GetAsync(RequestUri);
            actualRequestAfterDelete.EnsureSuccessStatusCode();
            var stringAfterDeleteResponse = await actualRequestAfterDelete.Content.ReadAsStringAsync();
            var categoriesAfterDelete = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(stringAfterDeleteResponse);
           
            httpResponse.EnsureSuccessStatusCode();
            categoriesAfterDelete.Count().Should().Be(categoriesBeforeDelete.Count() - 1);
            ;
        }
    }
}
