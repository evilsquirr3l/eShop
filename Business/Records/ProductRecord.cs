using System.ComponentModel.DataAnnotations;
using Database.Entities;

namespace Business.Records;

public record ProductRecord : BaseRecord
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string PictureUrl { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than 0.")]
    public int Quantity { get; set; }

    public virtual Category Category { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Range(typeof(decimal), "0", "1000000000000000000", ErrorMessage = "{0} must be a decimal/number between {1} and {2}.")]
    public decimal Price { get; set; }

    public virtual Discount Discount { get; set; }

    public int DiscountId { get; set; }
}