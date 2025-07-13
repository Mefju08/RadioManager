using RadioManager.Domain.Exceptions;

namespace RadioManager.Domain.Shows.Exceptions
{
    public sealed class NegativeDurationException : RadioManagerException
    {
        public NegativeDurationException(int durationMinutes) :
            base($"Duration: {durationMinutes} must be positive.")
        {
        }
    }
}
