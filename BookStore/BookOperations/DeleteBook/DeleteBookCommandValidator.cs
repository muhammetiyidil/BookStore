using FluentValidation;

namespace BookStore.BookOperations.DeleteBook
{
    public class DeleteBookCommandValidator : AbstractValidator<DeleteBookQuery>
    {
        public DeleteBookCommandValidator() 
        {
            RuleFor(command => command.Model.Title).MinimumLength(2);
        }
    }
}
