namespace Business.Records;

public record QueryStringParameters
{
    private const int MaxPageSize = 50;
    public int CurrentPage { get; set; } = 1;

    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}