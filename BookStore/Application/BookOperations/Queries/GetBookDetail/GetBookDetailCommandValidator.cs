using FluentValidation;

namespace BookStore.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailCommandValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}
