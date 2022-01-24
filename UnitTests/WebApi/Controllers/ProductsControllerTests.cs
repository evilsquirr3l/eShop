using System.Threading.Tasks;
using Business.Interfaces;
using Business.Records;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using WebApi.Controllers;

#pragma warning disable CS8602

namespace UnitTests.WebApi.Controllers;

[TestFixture]
public class ProductsControllerTests
{
    private ProductsController _productsController;
    private Mock<IProductService> _productService;
    
    [SetUp]
    public void SetUp()
    {
        _productService = new Mock<IProductService>();
        _productsController = new ProductsController(_productService.Object);
    }

    [Test]
    public async Task GetProduct_WhenProductExists_ReturnsOkObjectResultWithProduct()
    {
        var id = 1;
        var productRecord = new ProductRecord {Id = id, Name = "test"};
        _productService.Setup(x => x.GetProductAsync(id)).ReturnsAsync(productRecord);

        var result = await _productsController.GetProduct(id);

        result.Should().BeOfType<ActionResult<ProductRecord>>();
        (result as ActionResult<ProductRecord>).Value.Should().Be(productRecord);
    }
    
    [Test]
    public async Task GetProduct_WhenProductDoesntExist_ReturnsNotFound()
    {
        var id = 999;

        var result = await _productsController.GetProduct(id);

        result.Result.Should().BeOfType<NotFoundResult>();
    }
    
    [Test]
    public async Task CreateProduct_ExecutesService_ReturnsCreatedAtAction()
    {
        var id = 1;
        var productRecord = new ProductRecord {Id = id, Name = "test", Description = "test"};
        _productService.Setup(x => x.CreateProductAsync(productRecord));

        var result = await _productsController.CreateProduct(productRecord);

        result.Should().BeOfType<CreatedAtActionResult>();
        (result as CreatedAtActionResult).RouteValues["id"].Should().Be(id);
        (result as CreatedAtActionResult).ActionName.Should().Be(nameof(_productsController.GetProduct));
    }
    
    [Test]
    public async Task UpdateProduct_WhenProductExists_CallsServiceAndReturnsNoContent()
    {
        var id = 1;
        var productRecord = new ProductRecord {Id = id, Name = "test"};
        _productService.Setup(x => x.GetProductAsync(id)).ReturnsAsync(productRecord);

        var result = await _productsController.UpdateProduct(id, productRecord);

        result.Should().BeOfType<NoContentResult>();
        _productService.Verify(x => x.UpdateProductAsync(id, productRecord), Times.Once);
    }
    
    [Test]
    public async Task UpdateProduct_IdsAreNotEqual_ReturnsBadRequest()
    {
        var id = 1;
        var productRecord = new ProductRecord {Id = 999, Name = "test"};

        var result = await _productsController.UpdateProduct(id, productRecord);

        result.Should().BeOfType<BadRequestResult>();
    }
    
    [Test]
    public async Task UpdateProduct_WhenProductDoesntExist_ReturnsNotFound()
    {
        var id = 999;

        var result = await _productsController.UpdateProduct(id, new ProductRecord {Id = id});

        result.Should().BeOfType<NotFoundResult>();
    }
    
    [Test]
    public async Task DeleteProduct_WhenProductExists_CallsServiceAndReturnsNoContent()
    {
        var id = 1;
        var productRecord = new ProductRecord {Id = id, Name = "test"};
        _productService.Setup(x => x.GetProductAsync(id)).ReturnsAsync(productRecord);

        var result = await _productsController.DeleteProduct(id);

        result.Should().BeOfType<NoContentResult>();
        _productService.Verify(x => x.DeleteProductAsync(id), Times.Once);
    }
    
    [Test]
    public async Task DeleteProduct_WhenProductDoesntExist_ReturnsNotFound()
    {
        var id = 999;

        var result = await _productsController.DeleteProduct(id);

        result.Should().BeOfType<NotFoundResult>();
    }
}