using System;

namespace Checkout.Application.Repositories
{
    internal class OrderRepositoryException : Exception
    {
        public OrderRepositoryException(string message) : base(message)
        {
        }
    }
}
