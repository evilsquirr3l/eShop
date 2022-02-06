using System.Threading.Tasks;
using Business.Records;
using Business.Validators;
using Data;
using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace UnitTests.Business.Validators;

[TestFixture]
public class ProductValidatorTests
{
    private IValidator<ProductRecord> _validator;
    private EShopDbContext _dbContext;

    [SetUp]
    public void SetUp()
    {
        _dbContext = new EShopDbContext(UnitTestsHelper.UseInmemoryDatabase());
        _validator = new ProductValidator(_dbContext);
    }

    [Test]
    public async Task ProductValidator_NameIsEmpty_HasError()
    {
        var productRecord = new ProductRecord {Name = string.Empty};

        var result = await _validator.TestValidateAsync(productRecord);

        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
    
    [Test]
    public async Task ProductValidator_DescriptionIsEmpty_HasError()
    {
        var productRecord = new ProductRecord {Description = string.Empty};

        var result = await _validator.TestValidateAsync(productRecord);

        result.ShouldHaveValidationErrorFor(x => x.Description);
    }
    
    [Test]
    public async Task ProductValidator_PriceIsLessThan0_HasError()
    {
        var productRecord = new ProductRecord {Price = -1};

        var result = await _validator.TestValidateAsync(productRecord);

        result.ShouldHaveValidationErrorFor(x => x.Price);
    }
    
    [Test]
    public async Task ProductValidator_QuantityIsLessThan0_HasError()
    {
        var productRecord = new ProductRecord {Quantity = -1};

        var result = await _validator.TestValidateAsync(productRecord);

        result.ShouldHaveValidationErrorFor(x => x.Quantity);
    }
    
    [Test]
    public async Task ProductValidator_CategoryDoesntExist_HasError()
    {
        var productRecord = new ProductRecord {Name = "test"};

        var result = await _validator.TestValidateAsync(productRecord);

        result.ShouldHaveValidationErrorFor(x => x.CategoryId);
    }
    
    [Test]
    public async Task ProductValidator_IsDeletedIsTrue_HasError()
    {
        var productRecord = new ProductRecord {Name = "test", IsDeleted = true};

        var result = await _validator.TestValidateAsync(productRecord);

        result.ShouldHaveValidationErrorFor(x => x.IsDeleted);
    }
}