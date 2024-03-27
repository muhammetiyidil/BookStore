using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Application.GenreOperations.Commands.UpdateGenre.UpdateGenreCommand;
using static BookStore.Application.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;
using static BookStore.Application.GenreOperations.Queries.GetGenres.GetGenresQuery;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class GenreController : ControllerBase
    {
        private readonly BookStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreController(BookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public List<GenresViewModel> GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_dbContext, _mapper);
            List<GenresViewModel> genres = query.Handle();
            return genres;
        }

        [HttpGet]
        [Route("GetGenreById{id}")]
        public GenreDetailViewModel GetGenresById(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_dbContext, _mapper);
            query.GenreId = id;
            GetGenreDetailValidator validator = new GetGenreDetailValidator();
            validator.ValidateAndThrow(query);
            return query.Handle();
        }

        [HttpPost]
        public IActionResult PostGenre([FromBody] CreateGenreModel model)
        {
            CreateGenreCommand command = new CreateGenreCommand(_dbContext);
            command.Model = model;
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public IActionResult PutGenre(int id, [FromBody] UpdateGenreModel model)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_dbContext);
            command.Id = id;
            command.Model = model;
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteGenre([FromBody] string name)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_dbContext);
            command.Name = name;
            DeleteGenreCommandValidator validator = new DeleteGenreCommandValidator();
            validator.ValidateAndThrow(command);
            command.Handle();
            return Ok();
        }
    }
}
