using FluentValidation;

namespace BookStore.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookQuery>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.Model.Title).MinimumLength(2);
        }
    }
}
