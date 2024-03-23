using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.PostBook;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using static BookStore.BookOperations.DeleteBook.DeleteBookQuery;
using static BookStore.BookOperations.PostBook.PostBookQuery;
using static BookStore.BookOperations.UpdateBook.UpdateBookQuery;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext context;
        private readonly GetBooksQuery booksQuery;

        public BookController(BookStoreDbContext context, GetBooksQuery booksQuery)
        {
            this.context = context;
            this.booksQuery = booksQuery;
        }

        [HttpGet]
        [Route("Books")]
        public IActionResult GetBooks()
        {
            return Ok(booksQuery.Handle());
        }

        [HttpGet]
        [Route("BookById{id}")]
        public IActionResult GetBookById(int id)
        {
            try
            {
                GetBookDetailQuery query = new GetBookDetailQuery(context);
                return Ok(query.Handle(id));
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] PostBookModel book)
        {
            PostBookQuery command = new PostBookQuery(context); 
            try
            {
                command.Model = book;
                command.Handle();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdatedViewModel updatedBook)
        {
            try
            {
                UpdateBookQuery query = new UpdateBookQuery(context);
                query.Handle(id, updatedBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook([FromBody] DeletedViewModel deletedBook)
        {
            try
            {
                DeleteBookQuery query = new DeleteBookQuery(context);
                query.Handle(deletedBook);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
