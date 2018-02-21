using System;

namespace Core.Commanding.Exceptions
{
    public class DispatcherException : Exception
    {
        public DispatcherException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
