using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using BookStore.BookOperations.GetBooks;
using Microsoft.EntityFrameworkCore;

namespace BookStore.BookOperations.PostBook
{
    public class PostBookQuery
    {
        private readonly BookStoreDbContext _dbContext;
        public PostBookModel Model { get; set; }

        public PostBookQuery(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Bu isimde kitap zaten mevcut");
            book = new Book();
            book.Title = Model.Title;
            book.PublishDate = Model.PublishDate;
            book.GenreId = Model.GenreId;
            book.PageCount = Model.PageCount;

            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();
        }


        public class PostBookModel
        {
            public string Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }
        }
    }

}
