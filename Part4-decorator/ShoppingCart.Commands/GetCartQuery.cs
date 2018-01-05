using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;

namespace ShoppingCart.Commands
{
    public class GetCartQuery : ICommand<CommandResponse<Model.ShoppingCart>>, IUserContextCommand
    {
        public Guid AuthenticatedUserId { get; set; }
    }
}
