using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Store.Commands;
using Store.Model;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : AbstractCommandController
    {
        public ProductController(ICommandDispatcher dispatcher) : base(dispatcher)
        {
            
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(typeof(StoreProduct), 200)]
        public async Task<IActionResult> Get([FromRoute] GetStoreProductQuery query) => await ExecuteCommand<StoreProduct>(query);
    }
}
