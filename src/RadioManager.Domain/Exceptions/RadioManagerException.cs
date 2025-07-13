namespace RadioManager.Domain.Exceptions
{
    public abstract class RadioManagerException : Exception
    {
        protected RadioManagerException(string message) : base(message)
        {
        }
    }
}
