using Business.Records;

namespace Business.Paging;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(
        this IQueryable<T> source,
        QueryStringParameters parameters)
    {
        return source
            .Skip((parameters.CurrentPage - 1) * parameters.PageSize)
            .Take(parameters.PageSize);
    }
}