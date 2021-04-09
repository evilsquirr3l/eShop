using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstraction;
using Business.Implementation.Services;
using Business.Models;
using Data.Entities;
using Data.Implementation;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace eShop.UnitTests.ServiceTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private Mock<AbstractValidator<ProductDto>> _validator;
        private Mock<IServiceHelper<Product>> _helper;
        private ProductService _service;
        private IMapper _mapper;
        private ShopDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<AbstractValidator<ProductDto>>();
            _validator.Setup(validator => validator.ValidateAsync(It.IsAny<ValidationContext<ProductDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _context = new ShopDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _helper = new Mock<IServiceHelper<Product>>();
            _mapper = UnitTestsHelper.CreateMapperProfile();
            _service = new ProductService(_mapper, _context, _validator.Object, _helper.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.Products.CountAsync();

            _service.GetAllAsync().Result.Count().Should().Be(numberOfItemsInDatabase);
        }

        [Test]
        public async Task GetByIdAsync_ItemExists_ReturnsCategoryDto()
        {
            var actual = await _service.GetByIdAsync(1);

            actual.Should().BeOfType<ProductDto>();
        }

        [Test]
        public async Task GetByIdAsync_ItemDoesNotExist_ReturnsNull()
        {
            var actual = await _service.GetByIdAsync(1337);

            actual.Should().BeNull();
        }

        [Test]
        public async Task AddAsync_AddsItemToDatabase()
        {
            var numberOfItemsInDatabase = await _context.Products.CountAsync();
            await _service.AddAsync(new ProductDto { Name = "Testie" });

            _context.Products.CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
            _validator.VerifyAll();
        }

        [Test]
        public async Task DeleteByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Products.CountAsync();
            await _service.DeleteByIdAsync(1);

            _context.Products.CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Products.FindAsync(1).Result.Should().BeNull();
        }

        [Test]
        public async Task UpdateAsync_UpdateWithId1_ModelIsUpdated()
        {
            var productBeforeUpdate = await _context.Products.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            await _service.UpdateAsync(1, new ProductDto { Id = 1, Name = "Test" });

            var productAfterUpdate = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            productAfterUpdate.Id.Should().Be(productBeforeUpdate.Id);
            productAfterUpdate.Name.Should().NotBe(productBeforeUpdate.Name);
            _validator.VerifyAll();
        }
    }
}