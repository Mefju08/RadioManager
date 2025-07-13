using FluentValidation;
using RadioManager.Application.Time;

namespace RadioManager.Application.Shows.Commands.Create
{
    public sealed class CreateShowCommandValidator : AbstractValidator<CreateShowCommand>
    {
        public CreateShowCommandValidator(IClock clock)
        {
            RuleFor(x => x.Presenter)
                .NotEmpty();

            RuleFor(x => x.Title)
                .NotEmpty();

            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0);

            RuleFor(x => x.StartTime)
                .Must(x => x > clock.Now()).WithMessage("The date must be future");
        }
    }
}
