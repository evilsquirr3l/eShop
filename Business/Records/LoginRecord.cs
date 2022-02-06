namespace Business.Records;

public record LoginRecord
{
    public string Email { get; set; }
    
    public string Password { get; set; }
}