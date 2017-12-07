using System.Net;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Checkout.Commands;
using Checkout.Model;
using Microsoft.AspNetCore.Mvc;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class CheckoutController : AbstractCommandController
    {
        public CheckoutController(ICommandDispatcher dispatcher) : base(dispatcher)
        {
            
        }

        [HttpPost("createOrder")]
        [ProducesResponseType(typeof(Order), (int) HttpStatusCode.OK)]
        public async Task<IActionResult> Post() => await ExecuteCommand<CreateOrderCommand, Order>();

        [HttpPut("pay/{orderId}")]
        public async Task<IActionResult> Put([FromRoute] MakePaymentCommand command) => await ExecuteCommand(command);
    }
}
