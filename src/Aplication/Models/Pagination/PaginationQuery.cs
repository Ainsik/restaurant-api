namespace Application.Models.Pagination;
public class PaginationQuery
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
