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
        private CartsController _cartsController;

        [SetUp]
        public void SetUp()
        {
            _cartService = new Mock<ICrudInterface<CartDto>>();

            _cartsController = new CartsController(_cartService.Object);
        }
        
        [Test]
        public void GetAll_ReturnsElementsAsJson()
        {
            _cartService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<CartDto>());

            _cartsController.GetAll().Result.Should().BeOfType<ActionResult<IEnumerable<CartDto>>>();
        }

        [Test]
        public void GetById_CartWithId1Exists_ReturnsCart()
        {
            _cartService.Setup(s => s.GetByIdAsync(1)).ReturnsAsync(new CartDto {Id = 1});

            _cartsController.GetById(1).Result.Should().BeOfType<ActionResult<CartDto>>();
        }

        [Test]
        public async Task GetById_ItemDoesNotExist_ThrowsValidationError()
        {
            _cartService.Setup(s => s.GetByIdAsync(999)).ThrowsAsync(new ValidationException());

            await _cartsController.Invoking(c => c.GetById(999)).Should().ThrowAsync<ValidationException>();
        }

        [Test]
        public async Task Update_InvokesUpdateInService()
        {
            var cart = new CartDto {Id = 1};
            _cartService.Setup(s => s.UpdateAsync(1, cart));

            var result = await _cartsController.Update(1, cart);
            
            _cartService.Verify(s => s.UpdateAsync(1, cart), Times.Once);
            result.Should().BeOfType<OkResult>();
        }
        
        [Test]
        public void Update_ServiceThrowsValidationError_ThrowsException()
        {
            var cart = new CartDto {Id = 1, TotalPrice = -228};
            _cartService.Setup(s => s.UpdateAsync(1, cart)).ThrowsAsync(new ValidationException());
            
            _cartsController.Invoking(c => c.Update(1, cart)).Should().ThrowAsync<ValidationException>();
        }
        
        [Test]
        public async Task Delete_InvokesDeleteInService()
        {
            _cartService.Setup(s => s.DeleteByIdAsync(1));

            var result = await _cartsController.Delete(1);
            
            _cartService.Verify(s => s.DeleteByIdAsync(1), Times.Once);
            result.Should().BeOfType<OkResult>();
        }
    }
}