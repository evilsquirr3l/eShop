namespace Business.Records;

public record CartItemRecord : BaseRecord
{
    public UserRecord UserRecord { get; set; }

    public int UserId { get; set; }

    public int Quantity { get; set; }

    public ProductRecord ProductRecord { get; set; }

    public int ProductId { get; set; }
}