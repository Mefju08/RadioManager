using RadioManager.Domain.Shows.Exceptions;

namespace RadioManager.Domain.Shows.ValueObjects
{
    public sealed record TimeSlot
    {
        public DateTime StartTime { get; }
        public int DurationMinutes { get; }
        public DateTime EndTime => StartTime.AddMinutes(DurationMinutes);
        private TimeSlot(DateTime startTime, int durationMinutes)
        {
            StartTime = startTime;
            DurationMinutes = durationMinutes;
        }
        public static TimeSlot Create(DateTime startTime, int durationMinutes)
        {
            if (durationMinutes <= 0)
                throw new NegativeDurationException(durationMinutes);

            return new(startTime, durationMinutes);
        }

        public bool IsOverlapWith(TimeSlot timeSlot)
        {
            return timeSlot.StartTime < EndTime && StartTime < timeSlot.EndTime;
        }
    }
}
