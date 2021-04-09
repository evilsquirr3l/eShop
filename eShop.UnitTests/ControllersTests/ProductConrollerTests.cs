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
    public class ProductControllerTests
    {
        private Mock<ICrudInterface<ProductDto>> _productService;
        private ProductsController _productsController;

        [SetUp] 
        public void SetUp()
        {
            _productService = new Mock<ICrudInterface<ProductDto>>();

            _productsController = new ProductsController(_productService.Object);
        }

        [Test]
        public void GetAll_ReturnsElementsAsJson()
        {
            _productService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<ProductDto>());

            _productsController.GetAll().Result.Should().BeOfType<ActionResult<IEnumerable<ProductDto>>>();
        }

        [Test]
        public void GetById_ProductWithId1Exists_ReturnsProduct()
        {
            _productService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new ProductDto { Id = 1 });

            _productsController.GetById(1).Result.Should().BeOfType<ActionResult<ProductDto>>();
        }

        [Test]
        public async Task GetById_ItemDoesNotExist_ThrowsValidationError()
        {
            _productService.Setup(s => s.GetByIdAsync(999)).ThrowsAsync(new ValidationException());

            await _productsController.Invoking(c => c.GetById(999)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Update_InvokesUpdateInService()
        {
            var product = new ProductDto { Id = 1 };
            _productService.Setup(s => s.UpdateAsync(1, product));

            var result = await _productsController.Update(1, product);

            _productService.Verify(s => s.UpdateAsync(1, product), Times.Once);
            result.Should().BeOfType<OkResult>();
        }

        [Test]
        public void Update_ServiceThrowsValidationError_ReturnsBadRequestObjectResult()
        {
            var product = new ProductDto{ Id = 1, Name = "" };
            _productService.Setup(s => s.UpdateAsync(1, product)).ThrowsAsync(new ValidationException());

            _productsController.Invoking(c => c.Update(1, product)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Delete_InvokesDeleteInService()
        {
            _productService.Setup(s => s.DeleteByIdAsync(1));

            var result = await _productsController.Delete(1);

            _productService.Verify(s => s.DeleteByIdAsync(1), Times.Once);
            result.Should().BeOfType<OkResult>();
        }
    }
}