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
public class CategoryServiceTests
{
    private CategoryService _categoryService;
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
        
        _categoryService = new CategoryService(_dbContext, mapper, _dateTimeProvider.Object);
    }

    [TestCase(1, "testCategoryDescription", "testCategory")]
    public async Task GetCategoryAsync_WithId1_ReturnsCorrectCategoryWithDetails(int categoryId, string description, string categoryName)
    {
        await CreateTestCategoryWithId(categoryId);

        var result = await _categoryService.GetCategoryAsync(categoryId);

        result.Should().BeOfType<CategoryRecord>();
        result.Id.Should().Be(categoryId);
        result.Name.Should().Be(categoryName);
        result.Description.Should().Be(description);
        result.IsDeleted.Should().BeFalse();
    }
    
    [TestCase(1)]
    public async Task GetCategoryAsync_CategoryIsDeleted_ReturnsNull(int categoryId)
    {
        await _dbContext.Categories.AddAsync(new Category {Id = categoryId, Name = "test", Description = "test", IsDeleted = true});
        await _dbContext.SaveChangesAsync();

        var result = await _categoryService.GetCategoryAsync(categoryId);

        result.Should().BeNull();
    }

    [TestCase("testCategory", "description")]
    public async Task CreateCategoryAsync_WithValues_CreatesCategory(string name, string description)
    {
        var category = new CategoryRecord()
        {
            Description = description, Name = name
        };

        await _categoryService.CreateCategoryAsync(category);

        var entity = await _dbContext.Categories.FindAsync(1);
        entity.Should().NotBeNull();
        entity.Name.Should().Be(name);
        entity.Description.Should().Be(description);
        entity.CreatedAt.Should().Be(CurrentTime);
        entity.ModifiedAt.Should().Be(CurrentTime);
    }
    
    [TestCase(1, "updatedCategory", "updatedDescription")]
    public async Task UpdateCategoryAsync_WithValues_UpdatesCategory(int id, string name, string description)
    {
        await CreateTestCategoryWithId(id);
        var categoryAfterUpdate = new CategoryRecord()
        {
            Id = id, Description = description, Name = name
        };

        await _categoryService.UpdateCategoryAsync(id, categoryAfterUpdate);

        var category = await _dbContext.Categories.FindAsync(id);
        category.Should().NotBeNull();
        category.Name.Should().Be(name);
        category.Description.Should().Be(description);
        category.ModifiedAt.Should().Be(CurrentTime);
    }
    
    [TestCase(1)]
    public async Task DeleteCategoryAsync_WithId1_DeletesCategory(int id)
    {
        await CreateTestCategoryWithId(id);

        await _categoryService.DeleteCategoryAsync(id);

        var category = await _dbContext.Categories.FindAsync(id);
        category.Should().NotBeNull();
        category.IsDeleted.Should().BeTrue();
        category.ModifiedAt.Should().Be(CurrentTime);
    }
    
    private async Task CreateTestCategoryWithId(int id)
    {
        var category = new Category {Id = id, Name = "testCategory", Description = "testCategoryDescription"};
        
        await _dbContext.Categories.AddAsync(category);
        await _dbContext.SaveChangesAsync();
        _dbContext.Entry(category).State = EntityState.Detached;
    }
}