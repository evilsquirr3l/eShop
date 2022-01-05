namespace Database.Entities;

public class OrderDetails : BaseEntity
{
    public User User { get; set; }

    public int UserId { get; set; }

    public decimal Total { get; set; }

    public PaymentDetails PaymentDetails { get; set; }

    public int PaymentId { get; set; }
}