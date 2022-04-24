using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class CategoriesConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category
            {
                Id = 1,
                Name = "Shoes",
                Description = "Shoes category",
                CreatedAt = new DateTime(2022, 4, 24),
                ModifiedAt = new DateTime(2022, 4, 24),
            },
            new Category
            {
                Id = 2,
                Name = "Suits",
                Description = "Suits category",
                CreatedAt = new DateTime(2022, 4, 23),
                ModifiedAt = new DateTime(2022, 4, 23),
            });
    }
}