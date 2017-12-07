using System;

namespace Core.Model
{
    public class ApplicationException : Exception
    {
        public ApplicationException(string message) : base(message)
        {
        }

        public ApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
