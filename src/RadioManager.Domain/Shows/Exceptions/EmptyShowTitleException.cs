using RadioManager.Domain.Exceptions;

namespace RadioManager.Domain.Shows.Exceptions
{
    public sealed class EmptyShowTitleException : RadioManagerException
    {
        public EmptyShowTitleException() : base("Show title can not be empty.")
        {
        }
    }
}
