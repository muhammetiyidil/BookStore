using BookStore.BookOperations.GetBooks;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext dbContext;

        public GetBookDetailQuery (BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public BookDetailViewModel Handle(int id)
        {
            var book = dbContext.Books.SingleOrDefault(x => x.Id == id);
            if (book == null)
            {
                throw new InvalidOperationException("Kitap mevcut değil!");
            }
            BookDetailViewModel viewModel = new BookDetailViewModel();
            viewModel.Title = book.Title;
            viewModel.PageCount = book.PageCount;
            viewModel.PublishDate = book.PublishDate.ToString("dd/MM/yyy");
            viewModel.Genre = ((GenreEnum)book.GenreId).ToString();
            return viewModel;
        }

        public class BookDetailViewModel
        {
            public string Title { get; set; }
            public string Genre { get; set; }
            public int PageCount { get; set; }
            public string PublishDate { get; set; }
        }
    }
}
