using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using BookStore.BookOperations.GetBooks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BookStore.BookOperations.PostBook
{
    public class PostBookQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public PostBookModel Model { get; set; }

        public PostBookQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        
        public void Handle()
        {
            var book = _dbContext.Books.SingleOrDefault(x => x.Title == Model.Title);
            if (book is not null)
                throw new InvalidOperationException("Bu isimde kitap zaten mevcut");

            book = _mapper.Map<Book>(Model);

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
