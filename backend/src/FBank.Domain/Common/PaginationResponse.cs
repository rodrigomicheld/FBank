namespace FBank.Domain.Common
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T> Data { get; private set; }
        public int CurrentPage { get; private set; }
        public int TotalItems { get; private set; }
        public int TotalPages { get; set; }

        public PaginationResponse(IEnumerable<T> items, int currentPage, int totalItems, int pageSize)
        {
            Data = items;
            CurrentPage = currentPage;
            TotalItems = totalItems;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
        }
    }
}
