namespace GoatEdu.Core.CustomEntities;

public class PaginationOptions
{
    public int DefaultPageSize { get; set; } = 8;
    public int DefaultPageNumber { get; set; } = 1;
    public int MaxPageSize { get; set; }
}