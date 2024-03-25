using FluentValidation;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailCommandValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailCommandValidator() 
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}
