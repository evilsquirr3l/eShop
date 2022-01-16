namespace Data.Entities;

public class PaymentDetails : BaseEntity
{
    public int OrderDetailsId { get; set; }

    public virtual OrderDetails OrderDetails { get; set; }

    public int Amount { get; set; }

    public string Provider { get; set; }

    public string Status { get; set; }
}