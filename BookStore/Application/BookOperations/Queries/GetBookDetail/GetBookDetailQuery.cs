using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext dbContext;
        private readonly IMapper _mapper;
        public BookDetailViewModel Model { get; set; }
        public int Id { get; set; }
        public GetBookDetailQuery(BookStoreDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            _mapper = mapper;
        }

        public BookDetailViewModel Handle()
        {
            var book = dbContext.Books.Include(x => x.Genre).SingleOrDefault(x => x.Id == Id);
            if (book == null)
            {
                throw new InvalidOperationException("Kitap mevcut değil!");
            }

            BookDetailViewModel viewModel = _mapper.Map<BookDetailViewModel>(book);
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
