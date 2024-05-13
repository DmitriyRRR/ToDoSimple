namespace ToDoSimple.Models.Pagination
{
    public class PageViewModel
    {
        public int PageNumber { get; set; } = 0; // current page number
        public int TotalPages { get; set; } // total pages amount 
        public int PageSize { get; set; } // amount items on the each page
        public int TotalItemsCount { get; set; } = 0; //items amount??? isn't neccessery?
        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }
        public bool HasNextPage
        {
            get
            {
                return PageNumber < TotalPages;
            }
        }

        public IEnumerable<Note>? Notes { get; set; }

    }
}
