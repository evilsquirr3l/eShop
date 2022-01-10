namespace Business.Records;

public record OrderDetailsRecord : BaseRecord
{
    public UserRecord UserRecord { get; set; }

    public int UserId { get; set; }

    public decimal Total { get; set; }

    public PaymentDetailsRecord PaymentDetailsRecord { get; set; }

    public int PaymentId { get; set; }
}