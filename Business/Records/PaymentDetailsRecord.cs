namespace Business.Records;

public record PaymentDetailsRecord : BaseRecord
{
    public int OrderId { get; set; }

    public int Amount { get; set; }

    public string Provider { get; set; }

    public string Status { get; set; }
}