using RadioManager.Domain.Shows.Exceptions;

namespace RadioManager.Domain.Shows.ValueObjects
{
    public sealed record ShowTitle
    {
        public string Value { get; }

        private ShowTitle(string title) => Value = title;

        public static ShowTitle Create(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new EmptyShowTitleException();

            return new(title);
        }

        public static implicit operator string(ShowTitle title) => title.Value;
    }
}
