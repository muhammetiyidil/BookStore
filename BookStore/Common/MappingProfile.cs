using AutoMapper;
using static BookStore.BookOperations.GetBookDetail.GetBookDetailQuery;
using static BookStore.BookOperations.GetBooks.GetBooksQuery;
using static BookStore.BookOperations.PostBook.PostBookQuery;
using static BookStore.BookOperations.UpdateBook.UpdateBookQuery;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<PostBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString()))
                                            .ForMember(dest => dest.PublishTime, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy")));
        }
    }
}
