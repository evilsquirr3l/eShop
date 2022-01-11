namespace Business.Records;

public record UserAddressRecord : BaseRecord
{
    public UserRecord User { get; set; }

    public int UserId { get; set; }

    public string AddressLine1 { get; set; }

    public string AddressLine2 { get; set; }

    public string City { get; set; }

    public string PostalCode { get; set; }

    public string Country { get; set; }
}