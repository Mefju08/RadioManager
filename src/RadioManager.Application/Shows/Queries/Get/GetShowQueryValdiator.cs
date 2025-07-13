using FluentValidation;

namespace RadioManager.Application.Shows.Queries.Get
{
    public sealed class GetShowQueryValdiator : AbstractValidator<GetShowQuery>
    {
        public GetShowQueryValdiator()
        {
            RuleFor(x => x.ShowId)
                .NotEmpty();
        }
    }
}
