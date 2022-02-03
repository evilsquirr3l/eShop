using System;
using System.Threading.Tasks;
using Business.Interfaces;
using Business.Records;
using Business.Services;
using Data;
using Data.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace UnitTests.Business.Services;

[TestFixture]
public class ProductServiceTests
{
    private ProductService _productService;
    private EShopDbContext _dbContext;
    private Mock<IDateTimeProvider> _dateTimeProvider;

    private static readonly DateTime CurrentTime = new DateTime(2022, 1, 1);

    [SetUp]
    public void SetUp()
    {
        _dbContext = new EShopDbContext(UnitTestsHelper.UseInmemoryDatabase());
        
        _dateTimeProvider = new Mock<IDateTimeProvider>();
        _dateTimeProvider.Setup(x => x.GetCurrentTime()).Returns(CurrentTime);
        
        var mapper = UnitTestsHelper.CreateAutomapper();
        
        _productService = new ProductService(_dbContext, mapper, _dateTimeProvider.Object);
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

        var producTRecord = await _dbContext.Products.FindAsync(1);
        producTRecord.Should().NotBeNull();
        producTRecord.Name.Should().Be(name);
        producTRecord.Price.Should().Be(price);
        producTRecord.Quantity.Should().Be(quantity);
        producTRecord.Description.Should().Be(description);
        producTRecord.CreatedAt.Should().Be(CurrentTime);
        producTRecord.ModifiedAt.Should().Be(CurrentTime);
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

        var producTRecord = await _dbContext.Products.FindAsync(id);
        producTRecord.Should().NotBeNull();
        producTRecord.Name.Should().Be(name);
        producTRecord.Price.Should().Be(price);
        producTRecord.Quantity.Should().Be(quantity);
        producTRecord.Description.Should().Be(description);
        producTRecord.ModifiedAt.Should().Be(CurrentTime);
    }
    
    [TestCase(1)]
    public async Task DeleteProductAsync_WithId1_DeletesProduct(int id)
    {
        await CreateTestProductWithId(id);

        await _productService.DeleteProductAsync(id);

        var producTRecord = await _dbContext.Products.FindAsync(id);
        producTRecord.Should().NotBeNull();
        producTRecord.IsDeleted.Should().BeTrue();
        producTRecord.ModifiedAt.Should().Be(CurrentTime);
    }
    
    private async Task CreateTestProductWithId(int id)
    {
        var category = new Category {Id = id, Name = "testCategory", Description = "testCategoryDescription"};
        var producTRecord = new Product
            {Id = id, Name = "testProduct", Description = "testDescription", Category = category};
        await _dbContext.Products.AddAsync(producTRecord);
        await _dbContext.SaveChangesAsync();
        _dbContext.Entry(producTRecord).State = EntityState.Detached;
    }
}