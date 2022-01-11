using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business;
using Business.Records;
using Business.Services;
using Database;
using Database.Entities;
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

    [SetUp]
    public void SetUp()
    {
        var testProfile = new AutomapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(testProfile));
        var mapper = new Mapper(configuration);
        
        _dbContext = new EShopDbContext(UnitTestsHelper.GetUnitTestDbOptions());
        _productService = new ProductService(_dbContext, mapper);
    }
    
    [TestCase(1, "testProduct", 1, "testCategory")]
    public async Task GetProduct_WithId1_ReturnsCorrectProductWithDetails(int productId, string productName, int categoryId, string categoryName)
    {
        var category = new Category {Id = categoryId, Name = categoryName, Description = "test"};
        await _dbContext.Products.AddAsync(new Product {Id = productId, Name = productName, Description = "test", PictureUrl = "test.com", Category = category});
        await _dbContext.SaveChangesAsync();
        
        var result = await _productService.GetProductAsync(productId);

        result.Should().BeOfType<ProductRecord>();
        result.Id.Should().Be(productId);
        result.Name.Should().Be(productName);
        result.Category.Id.Should().Be(categoryId);
        result.Category.Name.Should().Be(categoryName);
    }
}