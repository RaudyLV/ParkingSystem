

namespace Application.Exceptions
{
    public class SessionAlreadyExistException : Exception
    {
        public SessionAlreadyExistException(string message) : base(message)
        {
            
        }
    }
}