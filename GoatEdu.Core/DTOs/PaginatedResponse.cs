using GoatEdu.Core.CustomEntities;

namespace GoatEdu.Core.DTOs
{
    public class PaginatedResponse<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
        public int? NextPageNumber => HasNextPage ? CurrentPage + 1 : (int?)null;
        public int? PreviousPageNumber => HasPreviousPage ? CurrentPage - 1 : (int?)null;
        public List<T> Items { get; set; }

        public PaginatedResponse(PagedList<T> pagedList)
        {
            CurrentPage = pagedList.CurrentPage;
            TotalPages = pagedList.TotalPages;
            PageSize = pagedList.PageSize;
            TotalCount = pagedList.TotalCount;
            Items = pagedList.ToList();
        }
    }
}