using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Checkout.Model;
using Core.Model;

namespace Checkout.Commands
{
    public class CreateOrderCommand : ICommand<CommandResponse<Order>>, IUserContextCommand
    {
        public Guid UserId { get; set; }
    }
}
