﻿using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;

namespace Checkout.Commands
{
    public class MakePaymentCommand : ICommand<CommandResponse>, IUserContextCommand
    {
        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }
    }
}
