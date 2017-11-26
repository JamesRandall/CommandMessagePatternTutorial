using System;
using AzureFromTheTrenches.Commanding.Abstractions;

namespace ShoppingCart.Commands
{
    public class GetCartQuery : ICommand<Model.ShoppingCart>
    {
        public Guid UserId { get; set; }
    }
}
