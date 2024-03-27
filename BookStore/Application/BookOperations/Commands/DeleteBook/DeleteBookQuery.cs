using BookStore.DBOperations;
using static BookStore.Application.BookOperations.Commands.UpdateBook.UpdateBookQuery;

namespace BookStore.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookQuery
    {
        private readonly BookStoreDbContext dbContext;
        public DeletedViewModel Model { get; set; }

        public DeleteBookQuery(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var book = dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book == null)
                throw new InvalidOperationException("Kitap mevcut değil!");
            dbContext.Books.Remove(book);
            dbContext.SaveChanges();
        }

        public class DeletedViewModel
        {
            public string Title { get; set; }
        }
    }
}
