namespace RadioManager.Domain.Types
{
    public class AggregateId<T>(T value) : IEquatable<AggregateId<T>>
    {
        public T Value { get; } = value;

        public bool Equals(AggregateId<T> other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T>.Default.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((AggregateId<T>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(Value);
        }
    }

    public class AggregateId : AggregateId<Guid>
    {
        private AggregateId(Guid value) : base(value) { }

        public static AggregateId Create() => new(Guid.NewGuid());
        public static AggregateId Create(Guid id) => new(id);

        public static implicit operator Guid(AggregateId id) => id.Value;
    }
}
