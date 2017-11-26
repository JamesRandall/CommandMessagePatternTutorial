using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;

namespace ShoppingCart.Commands
{
    public class AddToCartCommand : ICommand<CommandResponse>
    {
        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
