using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace SharedKernel;

public class PagedList<T> : List<T>
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        PageSize = pageSize;
        TotalCount = count;
        AddRange(items);
    }

    public static async Task<PagedList<T>> CreateAsync(
        IQueryable<T> source,
        int pageNumber,
        int pageSize
    )
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PagedList<T>(items, count, pageNumber, pageSize);
    }

    public static async Task<PagedList<T>> CreateAsync(
        IFindFluent<T, T> source,
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default
    )
    {
        var count = await source.CountDocumentsAsync(cancellationToken);
        var items = await source.Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync(cancellationToken);
        return new PagedList<T>(items, (int)count, pageNumber, pageSize);
    }

    public static PagedList<T> Map<TSource>(PagedList<TSource> source, IMapper mapper)
        where TSource : class
    {
        return new PagedList<T>(
            source.Select(mapper.Map<T>),
            source.TotalCount,
            source.CurrentPage,
            source.PageSize
        );
    }
}
