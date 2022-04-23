namespace Business.Records;

public record Page
{
    public int Skip { get; set; }

    public int Take { get; set; }

    public int Count { get; set; }

    public int Total { get; set; }
}