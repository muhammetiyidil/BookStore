using AutoMapper;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.PostBook;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using static BookStore.BookOperations.DeleteBook.DeleteBookQuery;
using static BookStore.BookOperations.GetBookDetail.GetBookDetailQuery;
using static BookStore.BookOperations.GetBooks.GetBooksQuery;
using static BookStore.BookOperations.PostBook.PostBookQuery;
using static BookStore.BookOperations.UpdateBook.UpdateBookQuery;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext context;
        private readonly IMapper mapper;

        public BookController(BookStoreDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("Books")]
        public List<BookViewModel> GetBooks()
        {
            GetBooksQuery booksQuery = new GetBooksQuery(context, mapper);
            var result = booksQuery.Handle();
            return result;
        }

        [HttpGet]
        [Route("BookById{id}")]
        public IActionResult GetBookById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(context, mapper);
            query.Id = id;
            GetBookDetailCommandValidator validator = new GetBookDetailCommandValidator();
            validator.ValidateAndThrow(query);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] PostBookModel book)
        {
            PostBookQuery command = new PostBookQuery(context, mapper);
            command.Model = book;
            PostBookCommandValidator validator = new PostBookCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdatedViewModel updatedBook)
        {
            
            UpdateBookQuery query = new UpdateBookQuery(context);
            query.Model = updatedBook;
            query.BookId = id;
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            validator.ValidateAndThrow(query);
            query.Handle();

            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook([FromBody] DeletedViewModel deletedBook)
        {
            DeleteBookQuery query = new DeleteBookQuery(context);
            query.Model = deletedBook;
            DeleteBookCommandValidator validator = new DeleteBookCommandValidator();
            validator.ValidateAndThrow(query);
            query.Handle();

            return Ok();
        }
    }
}
