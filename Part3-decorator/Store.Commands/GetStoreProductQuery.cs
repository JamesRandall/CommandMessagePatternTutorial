using System;
using AzureFromTheTrenches.Commanding.Abstractions;
using Store.Model;

namespace Store.Commands
{
    public class GetStoreProductQuery : ICommand<StoreProduct>
    {
        public Guid ProductId { get; set; }
    }
}
