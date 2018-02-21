
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.MicrosoftDependencyInjection;
using Core.Commanding;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.Application;
using Store.Commands;
using Store.Model;

namespace Store.Functions
{
    public static class GetStoreProduct
    {
        private static readonly IServiceProvider ServiceProvider;
        private static readonly AsyncLocal<ILogger> Logger = new AsyncLocal<ILogger>();
        
        static GetStoreProduct()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            MicrosoftDependencyInjectionCommandingResolver resolver = new MicrosoftDependencyInjectionCommandingResolver(serviceCollection);
            ICommandRegistry registry = resolver.UseCommanding();
            serviceCollection.UseCoreCommanding(resolver);
            serviceCollection.UseStore(() => ServiceProvider, registry, ApplicationModeEnum.Server);
            serviceCollection.AddTransient((sp) => Logger.Value);
            ServiceProvider = resolver.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        [FunctionName("GetStoreProduct")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, ILogger logger)
        {
            Logger.Value = logger;
            logger.LogInformation("C# HTTP trigger function processed a request.");
            
            IDirectCommandExecuter executer = ServiceProvider.GetService<IDirectCommandExecuter>();

            GetStoreProductQuery query = new GetStoreProductQuery
            {
                ProductId = Guid.Parse(req.GetQueryParameterDictionary()["ProductId"])
            };
            CommandResponse<StoreProduct> result = await executer.ExecuteAsync(query);
            return new OkObjectResult(result);
        }
    }
}