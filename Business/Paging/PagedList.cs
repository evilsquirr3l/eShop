namespace Business.Paging;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; private set; }
    public int TotalPages { get; private set; }
    public int PageSize { get; private set; }
    public int TotalCount { get; private set; }
    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    private PagedList(List<T> items, int totalCount, int currentPage, int pageSize)
    {
        TotalCount = totalCount;
        PageSize = pageSize;
        CurrentPage = currentPage;
        TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);

        AddRange(items);
    }

    public static PagedList<T> ToPagedList(List<T> items, int count, int pageNumber, int pageSize)
    {
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }
}