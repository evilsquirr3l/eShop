namespace Business.Records;

public record UserRecord : BaseRecord
{
    public string Username { get; set; }

    public string Token { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string PhoneNumber { get; set; }
}