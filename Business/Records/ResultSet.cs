namespace Business.Records;

public record ResultSet<T> where T: BaseRecord
{
    public IEnumerable<T> Data { get; set; }

    public Page Page { get; set; }
}