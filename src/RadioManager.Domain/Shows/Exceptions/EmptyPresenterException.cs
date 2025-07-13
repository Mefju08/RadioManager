using RadioManager.Domain.Exceptions;

namespace RadioManager.Domain.Shows.Exceptions
{
    public sealed class EmptyPresenterException : RadioManagerException
    {
        public EmptyPresenterException() : base("Presenter can not be empty.")
        {
        }
    }
}
