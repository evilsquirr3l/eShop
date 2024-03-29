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
public class CategoriesControllerTests
{
    private CategoriesController _categoriesController;
    private Mock<ICategoryService> _categoryService;

    [SetUp]
    public void SetUp()
    {
        _categoryService = new Mock<ICategoryService>();
        _categoriesController = new CategoriesController(_categoryService.Object,
            UnitTestsHelper.GetOptionsWithAppConfigurationsMock().Object);
    }

    [Test]
    public async Task GetCategory_WhenCategoryExists_ReturnsOkObjectResultWithCategory()
    {
        var id = 1;
        var categoryRecord = new CategoryRecord {Id = id, Name = "test"};
        _categoryService.Setup(x => x.GetCategoryAsync(id)).ReturnsAsync(categoryRecord);

        var result = await _categoriesController.GetCategory(id);

        result.Should().BeOfType<ActionResult<CategoryRecord>>();
        result.Value.Should().Be(categoryRecord);
    }

    [Test]
    public async Task GetCategory_WhenCategoryDoesntExist_ReturnsNotFound()
    {
        var id = 999;

        var result = await _categoriesController.GetCategory(id);

        result.Result.Should().BeOfType<NotFoundResult>();
    }

    [TestCase(0, 1)]
    public async Task GetCategories_ReturnsResultSet(int skip, int take)
    {
        var resultSet = new ResultSet<CategoryRecord>();
        _categoryService.Setup(x =>
                x.GetCategoryListAsync(It.Is<PaginationModel>(
                    x => x.Skip == skip && x.Take == take)))
            .ReturnsAsync(resultSet);

        var result = await _categoriesController.GetCategories(skip, take);
        result.Value.Should().Be(resultSet);
    }

    [Test]
    public async Task CreateCategory_ExecutesService_ReturnsCreatedAtAction()
    {
        var id = 1;
        var categoryRecord = new CategoryRecord {Id = id, Name = "test", Description = "test"};
        _categoryService.Setup(x => x.CreateCategoryAsync(categoryRecord));

        var result = await _categoriesController.CreateCategory(categoryRecord);

        result.Should().BeOfType<CreatedAtActionResult>();
        (result as CreatedAtActionResult).RouteValues["id"].Should().Be(id);
        (result as CreatedAtActionResult).ActionName.Should().Be(nameof(_categoriesController.GetCategory));
    }

    [Test]
    public async Task UpdateCategory_WhenCategoryExists_CallsServiceAndReturnsNoContent()
    {
        var id = 1;
        var categoryRecord = new CategoryRecord {Id = id, Name = "test"};
        _categoryService.Setup(x => x.GetCategoryAsync(id)).ReturnsAsync(categoryRecord);

        var result = await _categoriesController.UpdateCategory(id, categoryRecord);

        result.Should().BeOfType<NoContentResult>();
        _categoryService.Verify(x => x.UpdateCategoryAsync(id, categoryRecord), Times.Once);
    }

    [Test]
    public async Task UpdateCategory_IdsAreNotEqual_ReturnsBadRequest()
    {
        var id = 1;
        var categoryRecord = new CategoryRecord {Id = 999, Name = "test"};

        var result = await _categoriesController.UpdateCategory(id, categoryRecord);

        result.Should().BeOfType<BadRequestResult>();
    }

    [Test]
    public async Task UpdateCategory_WhenCategoryDoesntExist_ReturnsNotFound()
    {
        var id = 999;

        var result = await _categoriesController.UpdateCategory(id, new CategoryRecord {Id = id});

        result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public async Task DeleteCategory_WhenCategoryExists_CallsServiceAndReturnsNoContent()
    {
        var id = 1;
        var categoryRecord = new CategoryRecord {Id = id, Name = "test"};
        _categoryService.Setup(x => x.GetCategoryAsync(id)).ReturnsAsync(categoryRecord);

        var result = await _categoriesController.DeleteCategory(id);

        result.Should().BeOfType<NoContentResult>();
        _categoryService.Verify(x => x.DeleteCategoryAsync(id), Times.Once);
    }

    [Test]
    public async Task DeleteCategory_WhenCategoryDoesntExist_ReturnsNotFound()
    {
        var id = 999;

        var result = await _categoriesController.DeleteCategory(id);

        result.Should().BeOfType<NotFoundResult>();
    }
}