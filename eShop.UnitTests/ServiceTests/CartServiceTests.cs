using AutoMapper;
using Business.Abstraction;
using Business.Implementation;
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
using System.Net.Mime;
using System.Threading.Tasks;
using System.Xml;

namespace eShop.UnitTests.ServiceTests
{
    public class CartServiceTests
    {
        private static DbContextOptions<ShopDbContext> _options = new DbContextOptionsBuilder<ShopDbContext>()
            .UseInMemoryDatabase(databaseName: "ShopDb")
            .Options;
        private ShopDbContext _dbContext = new ShopDbContext(_options);
        private IMapper _mapper;
        private AutoMapperProfile _profile;
        private Mock<AbstractValidator<CartDto>> _validator;
        private Mock<IServiceHelper<Cart>> _helper;
        private CartService _service;

        [SetUp]
        public void Configure()
        {
            _profile = new AutoMapperProfile();
            _validator = new Mock<AbstractValidator<CartDto>>();
            _helper = new Mock<IServiceHelper<Cart>>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(_profile)));
        }

        [SetUpFixture]
        public class Seeder
        {
            public Seeder()
            {
            }

            [OneTimeSetUp]
            public void SetUp()
            {
                using (var context = new ShopDbContext(_options))
                {
                    var cart = new Cart {Id = 1, Products = new List<Product>(), TotalPrice = 0};
                    context.Carts.Add(cart);
                    context.SaveChanges();
                }
            }
        }

        [Test]
        public void GetByIdAsync_ReturnsCorrectType_WhenDbHasRequiredItem()
        {
            var actual = _service.GetByIdAsync(1).Result;

            Assert.IsInstanceOf<CartDto>(actual);
        }

        [Test]
        public void GetByIdAsync_ReturnsCorrectItem_WhenDbHasRequiredItem()
        {
            _service = new CartService(_mapper, _dbContext, _validator.Object, _helper.Object);
            var expected = new CartDto { Id = 1, Products = new List<ProductDto>(), TotalPrice = 0 };

            var actual = _service.GetByIdAsync(1).Result;

            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void GetByIdAsync_ReturnsNull_WhenDbDoesNotHaveRequiredItem()
        {
            _service = new CartService(_mapper, _dbContext, _validator.Object, _helper.Object);

            var actual = _service.GetByIdAsync(0).Result;

            Assert.AreEqual(null, actual);
        }

        [Test]
        public void GetAllAsync_ReturnsCorrectType_WhenDbHasRequiredItem()
        {
            var actual = _service.GetAllAsync().Result;

            Assert.IsInstanceOf<IEnumerable<CartDto>>(actual);
        }

        [Test]
        public void GetAllAsync_ReturnsCorrectItem_WhenDbHasRequiredItem()
        {
            _service = new CartService(_mapper, _dbContext, _validator.Object, _helper.Object);
            var expected = new List<CartDto>
            {
                new CartDto {Id = 1, Products = new List<ProductDto>(), TotalPrice = 0},
            };

            var actual = _service.GetAllAsync().Result;

            actual.Should().BeEquivalentTo(expected);
        }
      
        [Test]
        public async Task CreateAsync_AddsItemToDatabase_WhenDbHDoesNotHaveElementWithTheSameId()
        {   
            using (var context = new ShopDbContext(_options))
            {
                var service = new CartService(_mapper, _dbContext, _validator.Object, _helper.Object);
                var cartDto = new CartDto() { Id = 3, Products = new List<ProductDto>(), TotalPrice = 0};
                var expected = context.Carts.Count() + 1;
                await service.CreateAsync(cartDto);

                Assert.AreEqual(expected, context.Carts.Count());
            }
        }

        [Test] 
        public async Task DeleteByIdAsync_DeletesItemFromDatabase_WhenDbHasElementWithTheSameId()
        {
            using (var context = new ShopDbContext(_options))
            {
                var service = new CartService(_mapper, _dbContext, _validator.Object, _helper.Object);
                var expected = context.Carts.Count() - 1;
                await service.DeleteByIdAsync(3);

                Assert.AreEqual(expected, context.Carts.Count());
            }
        }

        [Test]
        public async Task UpdateAsync_UpdatesItemInDatabase_WhenDbHasSameElement()
        {
            var cart = _dbContext.Carts
                .FromSqlRaw("SELECT * FROM dbo.Blogs WHERE Id=1").ToList();
            var service = new CartService(_mapper, _dbContext, _validator.Object, _helper.Object);
            // check if local is not null 
                if (cart[0] != null)
                {
                    // detach
                    _dbContext.Entry(cart[0]).State = EntityState.Detached;
                }
                // save 
                _dbContext.SaveChanges();

                CartDto dto = new CartDto
                {
                    Id = cart[0].Id,
                    Products = new List<ProductDto>(),
                    TotalPrice = cart[0].TotalPrice
                };
                dto.TotalPrice = 4;
               

                await service.UpdateAsync(1, dto);
                dto = _mapper.Map<CartDto>(_dbContext.Carts
                    .FromSqlRaw("SELECT * FROM dbo.Blogs WHERE Id=1").ToList()[0]);
                dto.Id.Should().Be(1);
                dto.TotalPrice.Should().Be(4);

        }
    }
}