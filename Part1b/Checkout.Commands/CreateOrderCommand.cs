using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Checkout.Model;
using Core.Model;

namespace Checkout.Commands
{
    public class CreateOrderCommand : ICommand<CommandResponse<Order>>
    {
        public Guid UserId { get; set; }
    }
}
