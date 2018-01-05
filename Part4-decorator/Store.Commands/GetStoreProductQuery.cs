using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;
using Store.Model;

namespace Store.Commands
{
    public class GetStoreProductQuery : ICommand<CommandResponse<StoreProduct>>
    {
        public Guid ProductId { get; set; }
    }
}
