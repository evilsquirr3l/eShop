namespace Business.Records;

public record PaginationModel
{
    public int Skip { get; set; }

    public int Take { get; set; }
}