namespace GoatEdu.Core.QueriesFilter;

public class NotificationQueryFilter
{
    public Guid Id { get; set; }
    public string? NotificationName { get; set; }
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
}