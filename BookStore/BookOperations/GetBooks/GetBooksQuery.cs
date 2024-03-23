using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;

        public GetBooksQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.OrderBy(x => x.Id).ToList<Book>();
            List<BookViewModel> bookViewModels = new List<BookViewModel>();
            foreach (var book in bookList)
            {
                bookViewModels.Add(new BookViewModel
                {
                    Title = book.Title,
                    Genre = ((GenreEnum)book.GenreId).ToString(),
                    PageCount = book.PageCount,
                    PublishTime = book.PublishDate.ToString("dd/MM/yyyy"),
                });
            }
            return bookViewModels;
        }

        public class BookViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishTime { get; set; }
        }

    }
}
