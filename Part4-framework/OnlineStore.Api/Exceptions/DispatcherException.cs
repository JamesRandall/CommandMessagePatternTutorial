using System;

namespace OnlineStore.Api.Exceptions
{
    public class DispatcherException : Exception
    {
        public DispatcherException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
