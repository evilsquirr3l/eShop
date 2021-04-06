using AutoMapper;
using Business.Abstraction;
using Business.Implementation.Services;
using Business.Models;
using Data.Entities;
using Data.Implementation;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace eShop.UnitTests.ServiceTests
{
    [TestFixture]
    public class CartServiceTests
    {
        private Mock<AbstractValidator<CartDto>> _validator;
        private Mock<IServiceHelper<Cart>> _helper;
        private CartService _service;
        private IMapper _mapper;
        private ShopDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<AbstractValidator<CartDto>>();
            _validator.Setup(validator => validator.ValidateAsync(It.IsAny<ValidationContext<CartDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _context = new ShopDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _helper = new Mock<IServiceHelper<Cart>>();
            _mapper = UnitTestsHelper.CreateMapperProfile();
            _service = new CartService(_mapper, _context, _validator.Object, _helper.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.Carts.CountAsync();

            _service.GetAllAsync().Result.Count().Should().Be(numberOfItemsInDatabase);
        }
        
        [Test]
        public async Task GetByIdAsync_ItemExists_ReturnsCartDto()
        {
            var actual = await _service.GetByIdAsync(1);

            actual.Should().BeOfType<CartDto>();
        }
        
        [Test]
        public async Task GetByIdAsync_ItemDoesntExist_ReturnsNull()
        {
            var actual = await _service.GetByIdAsync(1337);

            actual.Should().BeNull();
        }

        [Test]
        public async Task AddAsync_AddsItemToDatabase()
        {
            var numberOfItemsInDatabase = await _context.Carts.CountAsync();
            await _service.AddAsync(new CartDto {Products = new List<ProductDto>(), TotalPrice = 1});

            _context.Carts.CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
            _validator.VerifyAll();
        }

        [Test]
        public async Task DeleteByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Carts.CountAsync();
            await _service.DeleteByIdAsync(1);
            
            _context.Carts.CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Carts.FindAsync(1).Result.Should().BeNull();
        }

        [Test]
        public async Task UpdateAsync_UpdateWithId1_ModelIsUpdated()
        {
            var cartBeforeUpdate = await _context.Carts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            await _service.UpdateAsync(1, new CartDto {Id = 1, Products = new List<ProductDto>(), TotalPrice = 1488});

            var cartAfterUpdate = await _context.Carts.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            cartAfterUpdate.Id.Should().Be(cartBeforeUpdate.Id);
            cartAfterUpdate.TotalPrice.Should().NotBe(cartBeforeUpdate.TotalPrice);
            _validator.VerifyAll();
        }
    }
}