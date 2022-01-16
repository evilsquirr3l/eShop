namespace Business.Records;

public record CategoryRecord : BaseRecord
{
    public string Name { get; set; }

    public string Description { get; set; }
}