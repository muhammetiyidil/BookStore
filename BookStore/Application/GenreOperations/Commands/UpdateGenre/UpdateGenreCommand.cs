using BookStore.DBOperations;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int Id { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == Id);
            if(genre == null) 
            {
                throw new InvalidOperationException("Kitap türü mevcut değil!");
            }
            if(_dbContext.Genres.Any(x=>x.Name.ToLower() == Model.Name.ToLower() &&  x.Id != Id))
            {
                throw new InvalidOperationException("Kitap türü zaten mevcut!");
            }
            genre.Name = Model.Name.Trim();
            _dbContext.SaveChanges();
        }

        public class UpdateGenreModel
        {
            public string Name { get; set; }
        }
    }
}
