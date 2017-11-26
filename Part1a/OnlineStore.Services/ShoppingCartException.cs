using System;

namespace OnlineStore.Services
{
    public class ShoppingCartException : Exception
    {
        public ShoppingCartException(string message) : base(message)
        {
        }
    }
}
