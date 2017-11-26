using System;

namespace OnlineStore.Services
{
    class CheckoutException : Exception
    {
        public CheckoutException(string message) : base(message)
        {
        }
    }
}
