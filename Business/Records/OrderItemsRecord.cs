namespace Business.Records;

public record OrderItemsRecord : BaseRecord
{
    public OrderDetailsRecord OrderDetails { get; set; }

    public int OrderDetailsId { get; set; }

    public ProductRecord Product { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}