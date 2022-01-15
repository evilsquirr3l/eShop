using Business.Records;
using Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Business.Validators;

public class ProductValidator : AbstractValidator<ProductRecord>
{
    private readonly EShopDbContext _dbContext;
    
    public ProductValidator(EShopDbContext dbContext)
    {
        _dbContext = dbContext;

        AddValidationRules();
    }

    private void AddValidationRules()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
        
        //throws error if IsDeleted == true
        RuleFor(x => x.IsDeleted).Empty();
        
        //throws error if category doesn't exist
        RuleFor(x => x.CategoryId).MustAsync(async (categoryId, cancellation) =>
        {
            var categoryExists =
                await _dbContext.Categories.AnyAsync(x => x.Id == categoryId, cancellationToken: cancellation);

            return categoryExists;
        }).WithMessage("Invalid category id!");
    }
}