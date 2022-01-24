using Business.Records;
using Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Business.Validators;

public class CategoryValidator : AbstractValidator<CategoryRecord>
{
    private readonly EShopDbContext _dbContext;

    public CategoryValidator(EShopDbContext dbContext)
    {
        _dbContext = dbContext;

        AddValidationRules();
    }

    private void AddValidationRules()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();

        //throws error if IsDeleted == true
        RuleFor(x => x.IsDeleted).Empty()
            .WithMessage("You can't create deleted category!");

        //throws error if category with the same name (ignoring case) already exists
        RuleFor(x => x.Name).MustAsync(async (categoryName, cancellation) =>
        {
            var categoryWithTheSameNameExists =
                // ReSharper disable once SpecifyStringComparison
                await _dbContext.Categories.AnyAsync(x => x.Name.ToLower() == categoryName.ToLower(),
                    cancellationToken: cancellation);

            return !categoryWithTheSameNameExists;
        }).WithMessage("Category with the same name already exists!");
    }
}