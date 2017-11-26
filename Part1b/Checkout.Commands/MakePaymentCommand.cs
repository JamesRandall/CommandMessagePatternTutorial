using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;

namespace Checkout.Commands
{
    public class MakePaymentCommand : ICommand<CommandResponse>
    {
        public Guid OrderId { get; set; }
    }
}
