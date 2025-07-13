using RadioManager.Domain.Exceptions;

namespace RadioManager.Domain.Shows.Exceptions
{
    public sealed class ShowScheduleConflictException : RadioManagerException
    {
        public ShowScheduleConflictException(string title) :
            base($"Show '{title}' conflicts with existing schedule.")
        {
        }
    }
}
