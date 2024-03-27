using BookStore.DBOperations;

namespace BookStore.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookQuery
    {

        private readonly BookStoreDbContext dbContext;
        public UpdatedViewModel Model { get; set; }
        public int BookId { get; set; }

        public UpdateBookQuery(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle()
        {
            var book = dbContext.Books.SingleOrDefault(x => x.Id == BookId);
            if (book == null)
            {
                throw new InvalidOperationException("Kitap mevcut değil!");
            }
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            if (book.Title != default)
                book.Title = Model.Title;
            dbContext.SaveChanges();

        }

        public class UpdatedViewModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
    }
}
