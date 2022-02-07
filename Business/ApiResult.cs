using System.Linq.Dynamic.Core;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Business;

public class ApiResult<T>
{
    /// <summary>
    /// Private constructor called by the CreateAsync method.
    /// </summary>
    private ApiResult(
        List<T> data,
        int count,
        int pageIndex,
        int pageSize,
        string? sortColumn, string? sortOrder, string? filterColumn, string? filterQuery)
    {
        Data = data;
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        SortColumn = sortColumn;
        SortOrder = sortOrder;
        FilterColumn = filterColumn;
        FilterQuery = filterQuery;
    }

    public static async Task<ApiResult<T>> CreateAsync(
        IQueryable<T> source,
        int pageIndex,
        int pageSize,
        string? sortColumn = null,
        string? sortOrder = null,
        string? filterColumn = null,
        string? filterQuery = null)
    {
        if (!string.IsNullOrEmpty(filterColumn) && !string.IsNullOrEmpty(filterQuery) && IsValidProperty(filterColumn))
        {
            source = source.Where($"{filterColumn}.Contains(@0)", filterQuery);
        }

        var count = await source.CountAsync();
        if (!string.IsNullOrEmpty(sortColumn) && IsValidProperty(sortColumn))
        {
            sortOrder = !string.IsNullOrEmpty(sortOrder) && sortOrder.ToUpper() == "ASC"
                ? "ASC"
                : "DESC";
            source = source.OrderBy(
                $"{sortColumn} {sortOrder}"
            );
        }

        source = source
            .Skip(pageIndex * pageSize)
            .Take(pageSize);
        var data = await source.ToListAsync();
        
        return new ApiResult<T>(data, count, pageIndex, pageSize, sortColumn, sortOrder, filterColumn, filterQuery);
    }

    /// <summary>
    /// Checks if the given property name exists
    /// to protect against SQL injection attacks
    /// </summary>
    public static bool IsValidProperty(
        string? propertyName,
        bool throwExceptionIfNotFound = true)
    {
        var prop = typeof(T).GetProperty(
            propertyName!,
            BindingFlags.IgnoreCase |
            BindingFlags.Public |
            BindingFlags.Static |
            BindingFlags.Instance);
        if (prop == null && throwExceptionIfNotFound)
        {
            throw new NotSupportedException($"ERROR: Property '{propertyName}' does not exist.");
        }

        return prop is not null;
    }

    public List<T> Data { get; set; }

    public int PageIndex { get; set; }

    public int PageSize { get; set; }

    public int TotalCount { get; set; }

    public int TotalPages { get; set; }

    public bool HasPreviousPage => PageIndex > 0;

    public bool HasNextPage => PageIndex + 1 < TotalPages;

    public string? SortColumn { get; set; }

    public string? SortOrder { get; set; }

    public string? FilterColumn { get; set; }

    public string? FilterQuery { get; set; }
}