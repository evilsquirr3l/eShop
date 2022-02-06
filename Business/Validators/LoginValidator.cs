using Business.Records;
using FluentValidation;

namespace Business.Validators;

public class LoginValidator : AbstractValidator<LoginRecord>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}