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
    
    [Test]
    public async Task GetProduct_WithId1_ReturnsProductRecordWithId1()
    {
        var productId = 1;
        var productName = "product1";
        await _dbContext.Products.AddAsync(new Product {Id = productId, Name = productName, Description = "test", PictureUrl = "test.com"});
        await _dbContext.SaveChangesAsync();
        
        var result = await _productService.GetProductAsync(productId);

        result.Should().BeOfType<ProductRecord>();
        result.Id.Should().Be(productId);
        result.Name.Should().Be(productName);
    }
}