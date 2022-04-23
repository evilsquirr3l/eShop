using Business.Records;

namespace Business.Paging;

public static class QueryableExtensions
{
    public static IQueryable<T> Paginate<T>(
        this IQueryable<T> source,
        PaginationModel paginationModel)
    {
        return source
            .Skip(paginationModel.Skip)
            .Take(paginationModel.Take);
    }
}