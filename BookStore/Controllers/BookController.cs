using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.PostBook;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using static BookStore.BookOperations.PostBook.PostBookQuery;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly GetBooksQuery _booksQuery;

        public BookController(BookStoreDbContext context, GetBooksQuery booksQuery)
        {
            _context = context;
            _booksQuery = booksQuery;
        }

        [HttpGet]
        [Route("Books")]
        public IActionResult GetBooks()
        {
            return Ok(_booksQuery.Handle());
        }

        [HttpGet]
        [Route("BookById{id}")]
        public IActionResult GetBookById(int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book == null)
            {
                return BadRequest();
            }
            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] PostBookModel book)
        {
            PostBookQuery command = new PostBookQuery(_context); 
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
        public IActionResult UpdateBook([FromBody] Book updatedBook)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == updatedBook.Id);
            if (book == null)
            {
                return BadRequest();
            }
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            if (book.PageCount != default)
                book.PageCount = updatedBook.PageCount;
            if (book.Title != default)
                book.Title = updatedBook.Title;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteBook([FromBody] int id)
        {
            var book = _context.Books.SingleOrDefault(x => x.Id == id);
            if (book == null)
                return BadRequest();
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
