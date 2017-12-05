using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;

namespace ShoppingCart.Commands
{
    public class AddToCartCommand : ICommand<CommandResponse>, IUserContextCommand
    {
        public Guid AuthenticatedUserId { get; set; }

        public Guid ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
