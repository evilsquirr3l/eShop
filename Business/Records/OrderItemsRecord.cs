namespace Business.Records;

public record OrderItemsRecord : BaseRecord
{
    public OrderDetailsRecord OrderDetailsRecord { get; set; }

    public int OrderDetailsId { get; set; }

    public ProductRecord ProductRecord { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }
}