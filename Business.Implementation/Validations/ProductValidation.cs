using Business.Models;
using FluentValidation;

namespace Business.Implementation.Validations
{
    public class ProductValidation : AbstractValidator<ProductDto>
    {
        public ProductValidation()
        {
            RuleFor(x => x.Category).NotNull();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.ImageUrl).NotEmpty();
        }
    }
}