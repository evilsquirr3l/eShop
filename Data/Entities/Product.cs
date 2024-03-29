using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string? PictureUrl { get; set; }

    public int Quantity { get; set; }

    public virtual Category Category { get; set; }

    public int CategoryId { get; set; }

    [Column(TypeName = "decimal(7, 2)")]
    public decimal Price { get; set; }

    public virtual Discount Discount { get; set; }

    public int DiscountId { get; set; }
}