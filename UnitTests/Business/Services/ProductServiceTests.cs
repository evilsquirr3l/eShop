using System;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Records;
using Business.Services;
using Data;
using Data.Entities;
using FluentAssertions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace UnitTests.Business.Services;

[TestFixture]
public class ProductServiceTests
{
    private ProductService _productService;
    private EShopDbContext _dbContext;
    private Mock<IValidator<ProductRecord>> _validator;
    private Mock<IDateTimeProvider> _dateTimeProvider;

    private static readonly DateTime CurrentTime = new DateTime(2022, 1, 1);

    [SetUp]
    public void SetUp()
    {

        _dbContext = new EShopDbContext(UnitTestsHelper.UseInmemoryDatabase());
        _validator = new Mock<IValidator<ProductRecord>>();
        
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _dateTimeProvider.Setup(x => x.GetCurrentTime()).Returns(CurrentTime);
        
        var mapper = UnitTestsHelper.CreateAutomapper(_dateTimeProvider.Object);
        
        _productService = new ProductService(_dbContext, mapper, _validator.Object, _dateTimeProvider.Object);
    }

    [TestCase(1, "testProduct", 1, "testCategory", "testDescription")]
    public async Task GetProductAsync_WithId1_ReturnsCorrectProductWithDetails(int productId, string productName, int categoryId, string categoryName, string description)
    {
        await CreateTestProductWithId(productId);

        var result = await _productService.GetProductAsync(productId);

        result.Should().BeOfType<ProductRecord>();
        result.Id.Should().Be(productId);
        result.Name.Should().Be(productName);
        result.Description.Should().Be(description);
        result.Category.Id.Should().Be(categoryId);
        result.Category.Name.Should().Be(categoryName);
        result.IsDeleted.Should().BeFalse();
    }
    
    [TestCase(1)]
    public async Task GetProductAsync_ProductIsDeleted_ReturnsNull(int productId)
    {
        await _dbContext.Products.AddAsync(new Product {Id = productId, Name = "test", Description = "test", IsDeleted = true});
        await _dbContext.SaveChangesAsync();

        var result = await _productService.GetProductAsync(productId);

        result.Should().BeNull();
    }

    [TestCase("testProduct", 100, 1, "description")]
    public async Task CreateProductAsync_WithValues_CreatesProduct(string name, decimal price, int quantity, string description)
    {
        var product = new ProductRecord
        {
            Description = description, Name = name, Price = price, Quantity = quantity,
        };

        await _productService.CreateProductAsync(product);

        var productEntity = await _dbContext.Products.FindAsync(1);
        productEntity.Should().NotBeNull();
        productEntity.Name.Should().Be(name);
        productEntity.Price.Should().Be(price);
        productEntity.Quantity.Should().Be(quantity);
        productEntity.Description.Should().Be(description);
        productEntity.CreatedAt.Should().Be(CurrentTime);
        productEntity.ModifiedAt.Should().Be(CurrentTime);
    }
    
    [TestCase(1, "updatedProduct", 100, 1, "updatedDescription")]
    public async Task UpdateProductAsync_WithValues_UpdatesProduct(int id, string name, decimal price, int quantity, string description)
    {
        await CreateTestProductWithId(id);
        var productAfterUpdate = new ProductRecord
        {
            Id = id, Description = description, Name = name, Price = price, Quantity = quantity,
        };

        await _productService.UpdateProductAsync(id, productAfterUpdate);

        var productEntity = await _dbContext.Products.FindAsync(id);
        productEntity.Should().NotBeNull();
        productEntity.Name.Should().Be(name);
        productEntity.Price.Should().Be(price);
        productEntity.Quantity.Should().Be(quantity);
        productEntity.Description.Should().Be(description);
        productEntity.ModifiedAt.Should().Be(CurrentTime);
    }
    
    [TestCase(1)]
    public async Task DeleteProductAsync_WithId1_DeletesProduct(int id)
    {
        await CreateTestProductWithId(id);

        await _productService.DeleteProductAsync(id);

        var productEntity = await _dbContext.Products.FindAsync(id);
        productEntity.Should().NotBeNull();
        productEntity.IsDeleted.Should().BeTrue();
        productEntity.ModifiedAt.Should().Be(CurrentTime);
    }
    
    private async Task CreateTestProductWithId(int id)
    {
        var category = new Category {Id = id, Name = "testCategory", Description = "testCategoryDescription"};
        var productEntity = new Product
            {Id = id, Name = "testProduct", Description = "testDescription", Category = category};
        await _dbContext.Products.AddAsync(productEntity);
        await _dbContext.SaveChangesAsync();
        _dbContext.Entry(productEntity).State = EntityState.Detached;
    }
}