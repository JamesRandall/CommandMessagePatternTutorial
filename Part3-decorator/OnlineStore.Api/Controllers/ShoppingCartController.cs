using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Commands;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartController : AbstractCommandController
    {
        public ShoppingCartController(ICommandDispatcher dispatcher) : base(dispatcher)
        {
            
        }

        [HttpGet]
        [ProducesResponseType(typeof(ShoppingCart.Model.ShoppingCart), 200)]
        public async Task<IActionResult> Get() => await ExecuteCommand<GetCartQuery, ShoppingCart.Model.ShoppingCart>();

        [HttpPut("{productId}/{quantity}")]
        public async Task<IActionResult> Put([FromRoute] AddToCartCommand command) => await ExecuteCommand(command);
        

        [HttpDelete]
        public async Task<IActionResult> Delete() => await ExecuteCommand<ClearCartCommand>();
    }
}
