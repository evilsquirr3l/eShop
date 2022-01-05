namespace Database.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public int Quantity { get; set; }

    public Category Category { get; set; }

    public int CategoryId { get; set; }

    public decimal Price { get; set; }

    public Discount Discount { get; set; }

    public int DiscountId { get; set; }
}