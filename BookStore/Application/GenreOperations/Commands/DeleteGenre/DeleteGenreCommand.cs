using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public string Name { get; set;}
        private readonly BookStoreDbContext _dbContext;

        public DeleteGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Name == Name);
            if (genre == null) 
            {
                throw new InvalidOperationException("Kitap türü mevcut değil!");
            }
            _dbContext.Genres.Remove(genre);
            _dbContext.SaveChanges();
        }
    }
}
