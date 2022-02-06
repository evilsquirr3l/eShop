namespace Business;

public class JwtSettings
{
    public string TokenKey { get; set; }

    public TimeSpan Lifetime { get; set; }
}