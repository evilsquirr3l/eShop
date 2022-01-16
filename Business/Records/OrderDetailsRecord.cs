namespace Business.Records;

public record OrderDetailsRecord : BaseRecord
{
    public UserRecord User { get; set; }

    public int UserId { get; set; }

    public decimal Total { get; set; }

    public PaymentDetailsRecord PaymentDetails { get; set; }

    public int PaymentId { get; set; }
}