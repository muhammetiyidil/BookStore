using BookStore.DBOperations;
using static BookStore.BookOperations.UpdateBook.UpdateBookQuery;

namespace BookStore.BookOperations.DeleteBook
{
    public class DeleteBookQuery
    {
        private readonly BookStoreDbContext dbContext;

        public DeleteBookQuery(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle(DeletedViewModel deletedModel)
        {
            var book = dbContext.Books.SingleOrDefault(x => x.Title == deletedModel.Title);
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
