using Business.Models;
using FluentValidation;

namespace Business.Implementation.Validations
{
    public class CartValidation : AbstractValidator<CartDto>
    {
        public CartValidation()
        {
            RuleFor(x => x.TotalPrice).GreaterThan(0);
        }
    }
}