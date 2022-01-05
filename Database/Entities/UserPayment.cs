namespace Database.Entities;

public class UserPayment : BaseEntity
{
    public User User { get; set; }

    public int UserId { get; set; }

    public string PaymentType { get; set; }

    public string Provider { get; set; }

    public string AccountNumber { get; set; }

    public string Expiry { get; set; }
}