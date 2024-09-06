namespace DotNetBoilerplate.Shared.Abstractions.Queries;

public interface IPagedQuery : IQuery
{
    enum SortOrderOptions
    {
        Asc = 1,
        Desc = 2
    }

    int Page { get; set; }
    int Results { get; set; }
    string OrderBy { get; set; }
    SortOrderOptions SortOrder { get; set; }
}

public interface IPagedQuery<T> : IPagedQuery, IQuery<T>
{
}