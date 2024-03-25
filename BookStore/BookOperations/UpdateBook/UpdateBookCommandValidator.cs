using FluentValidation;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookQuery>
    {
        public UpdateBookCommandValidator() 
        {
            RuleFor(com => com.model.Title).MinimumLength(2);
            RuleFor(com => com.model.GenreId).GreaterThan(0);
        }
    }
}
