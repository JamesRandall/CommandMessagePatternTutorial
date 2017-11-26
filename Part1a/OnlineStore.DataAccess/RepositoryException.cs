using System;

namespace OnlineStore.DataAccess
{
    class RepositoryException : Exception
    {
        public RepositoryException(string message) : base(message)
        {
        }
    }
}
