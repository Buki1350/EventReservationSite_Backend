namespace DotNetBoilerplate.Shared.Abstractions.Queries;

public class Paged<T> : PagedBase
{
    public Paged()
    {
        CurrentPage = 1;
        TotalPages = 1;
        ResultsPerPage = 10;
    }

    public Paged(IReadOnlyList<T> items,
        int currentPage, int resultsPerPage,
        int totalPages, long totalResults) :
        base(currentPage, resultsPerPage, totalPages, totalResults)
    {
        Items = items;
    }

    public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();

    public bool Empty => !Items.Any();

    public static Paged<T> AsEmpty => new();

    public static Paged<T> Create(IReadOnlyList<T> items,
        int currentPage, int resultsPerPage,
        int totalPages, long totalResults)
    {
        return new Paged<T>(items, currentPage, resultsPerPage, totalPages, totalResults);
    }

    public static Paged<T> From(PagedBase result, IReadOnlyList<T> items)
    {
        return new Paged<T>(items, result.CurrentPage, result.ResultsPerPage,
            result.TotalPages, result.TotalResults);
    }

    public Paged<TResult> Map<TResult>(Func<T, TResult> map)
    {
        return Paged<TResult>.From(this, Items.Select(map).ToList());
    }
}