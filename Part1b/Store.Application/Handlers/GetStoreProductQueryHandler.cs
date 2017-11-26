using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Store.Application.Repository;
using Store.Commands;
using Store.Model;

namespace Store.Application.Handlers
{
    internal class GetStoreProductQueryHandler : ICommandHandler<GetStoreProductQuery, StoreProduct>
    {
        private readonly IStoreProductRepository _repository;

        public GetStoreProductQueryHandler(IStoreProductRepository repository)
        {
            _repository = repository;
        }

        public Task<StoreProduct> ExecuteAsync(GetStoreProductQuery command, StoreProduct previousResult)
        {
            return _repository.GetAsync(command.ProductId);
        }
    }
}
