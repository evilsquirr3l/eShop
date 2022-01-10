namespace Business.Records;

public record DiscountRecord : BaseRecord
{
    public string Name { get; set; }

    public string Description { get; set; }

    public decimal DiscountPercent { get; set; }

    public bool IsActive { get; set; }
}