using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Store.Commands;
using Store.Model;

namespace OnlineStore.Api.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ICommandDispatcher _dispatcher;

        public ProductController(ICommandDispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("{id}")]
        public async Task<StoreProduct> Get(Guid id)
        {
            return await _dispatcher.DispatchAsync(new GetStoreProductQuery
            {
                ProductId = id
            });
        }
    }
}
