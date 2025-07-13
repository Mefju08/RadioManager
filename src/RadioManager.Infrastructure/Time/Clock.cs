using RadioManager.Application.Time;

namespace RadioManager.Infrastructure.Time
{
    internal sealed class Clock : IClock
    {
        public DateTime Now() => DateTime.UtcNow;

    }
}
