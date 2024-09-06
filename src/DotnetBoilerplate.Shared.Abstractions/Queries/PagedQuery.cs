namespace DotNetBoilerplate.Shared.Abstractions.Queries;

public abstract class PagedQuery : IPagedQuery
{
    public IPagedQuery.SortOrderOptions SortOrder { get; set; }
    public int Page { get; set; }
    public int Results { get; set; }
    public string OrderBy { get; set; } = "id";
}

public abstract class PagedQuery<T> : PagedQuery, IPagedQuery<Paged<T>>
{
}