using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;
using Store.Application.Repository;
using Store.Commands;
using Store.Model;

namespace Store.Application.Handlers
{
    internal class GetStoreProductQueryHandler : ICommandHandler<GetStoreProductQuery, CommandResponse<StoreProduct>>
    {
        private readonly IStoreProductRepository _repository;

        public GetStoreProductQueryHandler(IStoreProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResponse<StoreProduct>> ExecuteAsync(GetStoreProductQuery command, CommandResponse<StoreProduct> previousResult)
        {
            return CommandResponse<StoreProduct>.Ok(await _repository.GetAsync(command.ProductId));
        }
    }
}
