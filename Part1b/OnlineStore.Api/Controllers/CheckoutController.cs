using System;
using System.Net;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Checkout.Commands;
using Checkout.Model;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Extensions;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class CheckoutController : Controller
    {
        private readonly ICommandDispatcher _dispatcher;

        public CheckoutController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost("createOrder")]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post()
        {
            CommandResponse<Order> response = await _dispatcher.DispatchAsync(new CreateOrderCommand
            {
                UserId = this.GetUserId()
            });
            if (response.IsSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest(response.ErrorMessage);
        }

        [HttpPut("pay/{orderId}")]
        public async Task<IActionResult> Put(Guid orderId)
        {
            CommandResponse response = await _dispatcher.DispatchAsync(new MakePaymentCommand
            {
                OrderId = orderId
            });
            if (response.IsSuccess)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
