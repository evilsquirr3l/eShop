using System.Threading.Tasks;
using Business.Records;
using Business.Validators;
using Data;
using Data.Entities;
using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace UnitTests.Business.Validators;

[TestFixture]
public class CategoryValidatorTests
{
    private IValidator<CategoryRecord> _validator;
    private EShopDbContext _dbContext;

    [SetUp]
    public void SetUp()
    {
        _dbContext = new EShopDbContext(UnitTestsHelper.UseInmemoryDatabase());
        _validator = new CategoryValidator(_dbContext);
    }

    [Test]
    public async Task CategoryValidator_NameIsEmpty_HasError()
    {
        var categoryRecord = new CategoryRecord() {Name = string.Empty};

        var result = await _validator.TestValidateAsync(categoryRecord);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Test]
    public async Task CategoryValidator_DescriptionIsEmpty_HasError()
    {
        var categoryRecord = new CategoryRecord() {Description = string.Empty};

        var result = await _validator.TestValidateAsync(categoryRecord);

        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
    
    [Test]
    public async Task CategoryValidator_CategoryDoesntExist_HasError()
    {
        await _dbContext.Categories.AddAsync(new Category {Name = "test", Description = "test"});
        await _dbContext.SaveChangesAsync();
        var categoryRecord = new CategoryRecord() {Name = "test", Description = "test"};

        var result = await _validator.TestValidateAsync(categoryRecord);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Test]
    public async Task CategoryValidator_IsDeletedIsTrue_HasError()
    {
        var categoryRecord = new CategoryRecord() {Name = "test", IsDeleted = true};

        var result = await _validator.TestValidateAsync(categoryRecord);

        result.ShouldHaveValidationErrorFor(x => x.IsDeleted);
    }
}