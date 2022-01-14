using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Business;
using Business.Records;
using Business.Services;
using Data;
using Data.Entities;
using FluentAssertions;
using FluentValidation;
using Moq;
using NUnit.Framework;

namespace UnitTests.Business.Services;

[TestFixture]
public class ProductServiceTests
{
    private ProductService _productService;
    private EShopDbContext _dbContext;
    private Mock<IValidator<ProductRecord>> _validator;

    [SetUp]
    public void SetUp()
    {
        var testProfile = new AutomapperProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(testProfile));
        var mapper = new Mapper(configuration);

        _dbContext = new EShopDbContext(UnitTestsHelper.GetUnitTestDbOptions());
        _validator = new Mock<IValidator<ProductRecord>>();
        _productService = new ProductService(_dbContext, mapper, _validator.Object);
    }

    [TestCase(1, "testProduct", 1, "testCategory")]
    public async Task GetProductAsync_WithId1_ReturnsCorrectProductWithDetails(int productId, string productName,
        int categoryId, string categoryName)
    {
        var category = new Category {Id = categoryId, Name = categoryName, Description = "test"};
        await _dbContext.Products.AddAsync(new Product
            {Id = productId, Name = productName, Description = "test", Category = category});
        await _dbContext.SaveChangesAsync();

        var result = await _productService.GetProductAsync(productId);

        result.Should().BeOfType<ProductRecord>();
        result.Id.Should().Be(productId);
        result.Name.Should().Be(productName);
        result.Category.Id.Should().Be(categoryId);
        result.Category.Name.Should().Be(categoryName);
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
    }

    [Test]
    public async Task CreateProductAsync_WithAnyValues_ExecutesValidator()
    {
        var product = new ProductRecord() {Name = string.Empty, Description = string.Empty};
        _validator.Setup(x => x.ValidateAsync(It.IsAny<ValidationContext<ProductRecord>>(), CancellationToken.None));

        await _productService.CreateProductAsync(product);

        _validator.VerifyAll();
    }
}