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

namespace eShop.UnitTests.ServiceTests
{
    public class CartServiceTests
    {
        private DbContextOptions<ShopDbContext> _options;
        private ShopDbContext _dbContext;
        private IMapper _mapper;
        private AutoMapperProfile _profile;
        private Mock<AbstractValidator<CartDto>> _validator;
        private Mock<IServiceHelper<Cart>> _helper;
        private CartService _service;

        [SetUp]
        public void Configure()
        {
            _options = new DbContextOptionsBuilder<ShopDbContext>()
                .UseInMemoryDatabase(databaseName: "ShopDb")
                .Options;
            _dbContext = new ShopDbContext(_options);
            _profile = new AutoMapperProfile();
            _validator = new Mock<AbstractValidator<CartDto>>();
            _helper = new Mock<IServiceHelper<Cart>>();
            _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile(_profile)));
        }

        [SetUpFixture]
        public class Seeder
        {
            private DbContextOptions<ShopDbContext> _options;

            public Seeder()
            {
                _options = new DbContextOptionsBuilder<ShopDbContext>()
                    .UseInMemoryDatabase(databaseName: "ShopDb")
                    .Options;
            }

            [OneTimeSetUp]
            public void SetUp()
            {
                using (var context = new ShopDbContext(_options))
                {
                    context.Carts.Add(new Cart { Id = 1, Products = new List<Product>(), TotalPrice = 0 });
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
    }
}