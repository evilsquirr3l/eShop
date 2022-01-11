namespace Business.Records;

public record CartItemRecord : BaseRecord
{
    public UserRecord User { get; set; }

    public int UserId { get; set; }

    public int Quantity { get; set; }

    public ProductRecord Product { get; set; }

    public int ProductId { get; set; }
}