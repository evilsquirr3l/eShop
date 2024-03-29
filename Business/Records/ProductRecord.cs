namespace Business.Records;

public record ProductRecord : BaseRecord
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string? PictureUrl { get; set; }

    public int Quantity { get; set; }

    public virtual CategoryRecord Category { get; set; }

    public int CategoryId { get; set; }

    public decimal Price { get; set; }

    public virtual DiscountRecord Discount { get; set; }

    public int DiscountId { get; set; }
}