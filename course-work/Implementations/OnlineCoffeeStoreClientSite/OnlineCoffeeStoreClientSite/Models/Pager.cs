namespace OnlineCoffeeStoreClientSite.Models
{
    public class Pager
    {
        public int TotalItems { get; }
        public int CurrentPage { get; }
        public int PageSize { get; }
        public int TotalPages { get; }
        public int StartPage { get; }
        public int EndPage { get; }

        public Pager() {
        
        }

        public Pager(int totalItems, int page, int pageSize = 3) 
        {
            int totalPages = (int)Math.Ceiling((decimal)totalItems / (decimal)pageSize);
            int currentPage = page;

            int startPage = currentPage - 2 ;
            int endPage = currentPage + 3;

            if (startPage <= 0) 
            {
                endPage = endPage - (startPage-1) ;
                startPage = 1;
            
            }

            if (endPage>totalPages)
            {
                endPage = totalPages;
                if (endPage>10)
                {
                    startPage = endPage - 3;
                }
            }

            TotalItems = totalItems;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = totalPages;
            StartPage = startPage;
            EndPage = endPage;
        }
    }

}
