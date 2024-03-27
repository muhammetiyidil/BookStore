using FluentValidation;

namespace BookStore.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookQuery>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(com => com.Model.Title).MinimumLength(2);
            RuleFor(com => com.Model.GenreId).GreaterThan(0);
            RuleFor(com => com.BookId).GreaterThan(0);
        }
    }
}
