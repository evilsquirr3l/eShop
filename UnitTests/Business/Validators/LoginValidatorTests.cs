using System.Threading.Tasks;
using Business.Records;
using Business.Validators;
using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;

namespace UnitTests.Business.Validators;

[TestFixture]
public class LoginValidatorTests
{
    private IValidator<LoginRecord> _validator;

    [SetUp]
    public void SetUp()
    {
        _validator = new LoginValidator();
    }
    
    [Test]
    public async Task LoginValidator_EmailIsInvalid_HasError()
    {
        var loginValidator = new LoginRecord {Email = "invalidEmail"};

        var result = await _validator.TestValidateAsync(loginValidator);

        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Test]
    public async Task LoginValidator_EmailIsValid_DoesntHaveError()
    {
        var loginValidator = new LoginRecord {Email = "valid@email.com"};

        var result = await _validator.TestValidateAsync(loginValidator);

        result.ShouldNotHaveValidationErrorFor(x => x.Email);
    }
    
    [Test]
    public async Task LoginValidator_PasswordIsEmpty_HasError()
    {
        var loginValidator = new LoginRecord {Password = string.Empty};

        var result = await _validator.TestValidateAsync(loginValidator);

        result.ShouldHaveValidationErrorFor(x => x.Password);
    }
}