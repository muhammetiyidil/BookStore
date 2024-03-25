using FluentValidation;

namespace BookStore.BookOperations.PostBook
{
    public class PostBookCommandValidator : AbstractValidator<PostBookQuery>
    {
        public PostBookCommandValidator()
        {
            RuleFor(command => command.Model.GenreId).GreaterThan(0);
            RuleFor(command => command.Model.PageCount).GreaterThan(0);
            RuleFor(command => command.Model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(2);
        }
    }
}
