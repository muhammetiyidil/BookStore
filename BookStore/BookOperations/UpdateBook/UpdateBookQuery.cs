using BookStore.DBOperations;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookQuery
    {

        private readonly BookStoreDbContext dbContext;

        public UpdateBookQuery(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Handle(int id, UpdatedViewModel updatedModel)
        {
            var book = dbContext.Books.SingleOrDefault(x => x.Id == id);
            if (book == null)
            {
                throw new InvalidOperationException("Kitap mevcut değil!");
            }
            book.GenreId = updatedModel.GenreId != default ? updatedModel.GenreId : book.GenreId;
            if (book.Title != default)
                book.Title = updatedModel.Title;
            dbContext.SaveChanges();

        }

        public class UpdatedViewModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
        }
    }
}
