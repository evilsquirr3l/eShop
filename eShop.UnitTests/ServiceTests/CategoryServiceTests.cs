using System.Collections.Generic;
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
    public class CategoryServiceTests
    {
        private Mock<AbstractValidator<CategoryDto>> _validator;
        private Mock<IServiceHelper<Category>> _helper;
        private CategoryService _service;
        private IMapper _mapper;
        private ShopDbContext _context;

        [SetUp]
        public void SetUp()
        {
            _validator = new Mock<AbstractValidator<CategoryDto>>();
            _validator.Setup(validator => validator.ValidateAsync(It.IsAny<ValidationContext<CategoryDto>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult());
            _context = new ShopDbContext(UnitTestsHelper.GetUnitTestDbOptions());
            _helper = new Mock<IServiceHelper<Category>>();
            _mapper = UnitTestsHelper.CreateMapperProfile();
            _service = new CategoryService(_mapper, _context, _validator.Object, _helper.Object);
        }

        [Test]
        public async Task GetAllAsync_ReturnsAllElements()
        {
            var numberOfItemsInDatabase = await _context.Categories.CountAsync();

            _service.GetAllAsync().Result.Count().Should().Be(numberOfItemsInDatabase);
        }

        [Test]
        public async Task GetByIdAsync_ItemExists_ReturnsCategoryDto()
        {
            var actual = await _service.GetByIdAsync(1);

            actual.Should().BeOfType<CategoryDto>();
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
            var numberOfItemsInDatabase = await _context.Categories.CountAsync();
            await _service.AddAsync(new CategoryDto { Products = new List<ProductDto>(), Name = "Testie" });

            _context.Categories.CountAsync().Result.Should().Be(numberOfItemsInDatabase + 1);
            _validator.VerifyAll();
        }

        [Test]
        public async Task DeleteByIdAsync_DeleteWithId1_DeletesItem()
        {
            var numberOfItemsInDatabase = await _context.Categories.CountAsync();
            await _service.DeleteByIdAsync(1);

            _context.Categories.CountAsync().Result.Should().Be(numberOfItemsInDatabase - 1);
            _context.Categories.FindAsync(1).Result.Should().BeNull();
        }

        [Test]
        public async Task UpdateAsync_UpdateWithId1_ModelIsUpdated()
        {
            var categoryBeforeUpdate = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);

            await _service.UpdateAsync(1, new CategoryDto { Id = 1, Products = new List<ProductDto>(), Name = "Test" });

            var categoryAfterUpdate = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == 1);
            categoryAfterUpdate.Id.Should().Be(categoryBeforeUpdate.Id);
            categoryAfterUpdate.Name.Should().NotBe(categoryBeforeUpdate.Name);
            _validator.VerifyAll();
        }
    }
}