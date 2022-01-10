namespace Business.Records;

public record ProductRecord : BaseRecord
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string PictureUrl { get; set; }

    public int Quantity { get; set; }

    public CategoryRecord CategoryRecord { get; set; }

    public int CategoryId { get; set; }

    public decimal Price { get; set; }

    public DiscountRecord DiscountRecord { get; set; }

    public int DiscountId { get; set; }
}