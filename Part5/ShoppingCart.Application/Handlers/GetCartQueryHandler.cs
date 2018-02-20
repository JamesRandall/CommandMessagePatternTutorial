using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;
using ShoppingCart.Application.Repositories;
using ShoppingCart.Commands;

namespace ShoppingCart.Application.Handlers
{
    class GetCartQueryHandler : ICommandHandler<GetCartQuery, CommandResponse<Model.ShoppingCart>>
    {
        private readonly IShoppingCartRepository _repository;

        public GetCartQueryHandler(IShoppingCartRepository repository)
        {
            _repository = repository;
        }

        public async Task<CommandResponse<Model.ShoppingCart>> ExecuteAsync(GetCartQuery command, CommandResponse<Model.ShoppingCart> previousResult)
        {
            Model.ShoppingCart cart = await _repository.GetActualOrDefaultAsync(command.AuthenticatedUserId);
            return CommandResponse<Model.ShoppingCart>.Ok(cart);
        }
    }
}
