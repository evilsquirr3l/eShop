namespace Data.Entities;

public class UserAddress : BaseEntity
{
    public virtual User User { get; set; }

    public int UserId { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string City { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }
}