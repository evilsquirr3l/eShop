using Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations;

public class ProductsConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(
            new Product
            {
                Id = 1,
                CategoryId = 1,
                Description = "Shoes",
                Name = "Adidas",
                PictureUrl = "https://fadzrinmadu.github.io/hosted-assets/product-detail-page-design-with-image-slider-html-css-and-javascript/shoe_1.jpg",
                CreatedAt = new DateTime(2022, 4, 24),
                ModifiedAt = new DateTime(2022, 4, 24),
            },
            new Product
            {
                Id = 2,
                CategoryId = 2,
                Description = "Suit",
                Name = "Gosling's suite",
                PictureUrl = "https://www3.pictures.stylebistro.com/gi/Place+Beyond+Pines+Premiere+Arrivals+2012+NpkDUEe9LBRl.jpg",
                CreatedAt = new DateTime(2022, 4, 24),
                ModifiedAt = new DateTime(2022, 4, 24),
            });
    }
}