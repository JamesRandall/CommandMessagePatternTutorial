using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Extensions;
using ShoppingCart.Commands;


namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class ShoppingCartController : Controller
    {
        private readonly ICommandDispatcher _dispatcher;

        public ShoppingCartController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet]
        public async Task<ShoppingCart.Model.ShoppingCart> Get()
        {
            return await _dispatcher.DispatchAsync(new GetCartQuery
            {
                UserId = this.GetUserId()
            });
        }

        [HttpPut("{productId}/{quantity}")]
        public async Task<IActionResult> Put(Guid productId, int quantity)
        {
            CommandResponse response = await _dispatcher.DispatchAsync(new AddToCartCommand
            {
                UserId = this.GetUserId(),
                ProductId = productId,
                Quantity = quantity
            });
            if (response.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(response.ErrorMessage);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            CommandResponse response = await _dispatcher.DispatchAsync(new ClearCartCommand
            {
                UserId = this.GetUserId()                
            });
            if (response.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(response.ErrorMessage);
        }
    }
}
