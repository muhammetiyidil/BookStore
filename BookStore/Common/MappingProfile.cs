using AutoMapper;
using BookStore.Entities;
using static BookStore.Application.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;
using static BookStore.Application.BookOperations.Queries.GetBooks.GetBooksQuery;
using static BookStore.Application.BookOperations.Commands.PostBook.PostBookQuery;
using static BookStore.Application.BookOperations.Commands.UpdateBook.UpdateBookQuery;
using static BookStore.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;
using static BookStore.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;

namespace BookStore.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<PostBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name));
            CreateMap<Book, BookViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                                            .ForMember(dest => dest.PublishTime, opt => opt.MapFrom(src => src.PublishDate.ToString("dd/MM/yyyy")));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
        }
    }
}
