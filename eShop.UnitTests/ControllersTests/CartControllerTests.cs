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
    public class CartControllerTests
    {
        private Mock<ICrudInterface<CartDto>> _cartService;
        private CartController _cartController;

        [SetUp]
        public void SetUp()
        {
            _cartService = new Mock<ICrudInterface<CartDto>>();

            _cartController = new CartController(_cartService.Object);
        }
        
        [Test]
        public async Task GetAll_ReturnsElementsAsJson()
        {
            _cartService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<CartDto>());

            _cartController.GetAll().Result.Should().BeOfType<ActionResult<IEnumerable<CartDto>>>();
        }

        [Test]
        public async Task GetById_CartWithId1Exists_ReturnsCart()
        {
            _cartService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new CartDto {Id = 1});

            _cartController.GetById(1).Result.Should().BeOfType<ActionResult<CartDto>>();
        }

        [Test]
        public async Task GetById_ItemDoesntExist_ThrowsValidationError()
        {
            _cartService.Setup(s => s.GetByIdAsync(999)).ThrowsAsync(new ValidationException());

            await _cartController.Invoking(c => c.GetById(999)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Update_InvokesUpdateInService()
        {
            var cart = new CartDto {Id = 1};
            _cartService.Setup(s => s.UpdateAsync(1, cart));

            var result = await _cartController.Update(1, cart);
            
            _cartService.Verify(s => s.UpdateAsync(1, cart), Times.Once);
            result.Should().BeOfType<OkResult>();
        }
        
        [Test]
        public async Task Update_ServiceThrowsValidationError_ReturnsBadRequestObjectResult()
        {
            var cart = new CartDto {Id = 1, TotalPrice = -228};
            _cartService.Setup(s => s.UpdateAsync(1, cart)).ThrowsAsync(new ValidationException());

            _cartController.Update(1, cart).Result.Should().BeOfType<BadRequestObjectResult>();
        }
        
        [Test]
        public async Task Delete_InvokesDeleteInService()
        {
            _cartService.Setup(s => s.DeleteByIdAsync(1));

            var result = await _cartController.Delete(1);
            
            _cartService.Verify(s => s.DeleteByIdAsync(1), Times.Once);
            result.Should().BeOfType<OkResult>();
        }
    }
}