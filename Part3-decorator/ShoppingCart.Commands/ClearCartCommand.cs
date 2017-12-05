using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;

namespace ShoppingCart.Commands
{
    public class ClearCartCommand : ICommand<CommandResponse>, IUserContextCommand
    {
        public Guid AuthenticatedUserId { get; set; }
    }
}
