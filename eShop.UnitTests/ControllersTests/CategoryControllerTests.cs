using System.Collections.Generic;
using System.Threading.Tasks;
using Business.Abstraction;
using Business.Implementation;
using Business.Models;
using eShop.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace eShop.UnitTests.ControllersTests
{
    [TestFixture]
    public class CategoryControllerTests
    {
        private Mock<ICrudInterface<CategoryDto>> _categoryService;
        private CategoriesController _categoryController;

        [SetUp]
        public void SetUp()
        {
            _categoryService = new Mock<ICrudInterface<CategoryDto>>();

            _categoryController = new CategoriesController(_categoryService.Object);
        }

        [Test]
        public void GetAll_ReturnsElementsAsJson()
        {
            _categoryService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<CategoryDto>());

            _categoryController.GetAll().Result.Should().BeOfType<ActionResult<IEnumerable<CategoryDto>>>();
        }

        [Test]
        public void GetById_CategoryWithId1Exists_ReturnsProduct()
        {
            _categoryService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new CategoryDto { Id = 1 });

            _categoryController.GetById(1).Result.Should().BeOfType<ActionResult<CategoryDto>>();
        }

        [Test]
        public async Task GetById_ItemDoesNotExist_ThrowsValidationError()
        {
            _categoryService.Setup(s => s.GetByIdAsync(999)).ThrowsAsync(new ValidationException());

            await _categoryController.Invoking(c => c.GetById(999)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Update_InvokesUpdateInService()
        {
            var category = new CategoryDto { Id = 1 };
            _categoryService.Setup(s => s.UpdateAsync(1, category));

            var result = await _categoryController.Update(1, category);

            _categoryService.Verify(s => s.UpdateAsync(1, category), Times.Once);
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public void Update_ServiceThrowsValidationError_ReturnsBadRequestObjectResult()
        {
            var category = new CategoryDto { Id = 1, Name = "" };
            _categoryService.Setup(s => s.UpdateAsync(1, category)).ThrowsAsync(new ValidationException());

            _categoryController.Invoking(c => c.Update(1, category)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Delete_InvokesDeleteInService()
        {
            _categoryService.Setup(s => s.DeleteByIdAsync(1));

            var result = await _categoryController.Delete(1);

            _categoryService.Verify(s => s.DeleteByIdAsync(1), Times.Once);
            result.Should().BeOfType<OkResult>();
        }
    }
}