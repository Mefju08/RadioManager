using RadioManager.Domain.Shows.Exceptions;

namespace RadioManager.Domain.Shows.ValueObjects
{
    public sealed record Presenter
    {
        public string Value { get; }

        private Presenter(string presenter) => Value = presenter;

        public static Presenter Create(string presenter)
        {
            if (string.IsNullOrWhiteSpace(presenter))
                throw new EmptyPresenterException();

            return new(presenter);
        }

        public static implicit operator string(Presenter presenter) => presenter.Value;
    }
}
