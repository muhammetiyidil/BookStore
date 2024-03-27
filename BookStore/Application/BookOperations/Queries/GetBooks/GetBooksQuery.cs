using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetBooksQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public List<BookViewModel> Handle()
        {
            var bookList = _dbContext.Books.Include(x => x.Genre).OrderBy(x => x.Id).ToList();
            List<BookViewModel> bookViewModels = _mapper.Map<List<BookViewModel>>(bookList);
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
