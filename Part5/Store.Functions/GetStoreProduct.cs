
using System.IO;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.MicrosoftDependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Store.Application;
using Store.Commands;

namespace Store.Functions
{
    public static class GetStoreProduct
    {
        [FunctionName("GetStoreProduct")]
        public static Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(configure =>
            {
                configure.AddTraceSource()
            })
            ICommandingDependencyResolver resolver = new MicrosoftDependencyInjectionCommandingResolver(serviceCollection);
            ICommandRegistry registry = resolver.UseCommanding(new Options
            {
                //CommandExecutionExceptionHandler = typeof(CommandExecutionExceptionHandler)
            });
            serviceCollection.UseStore(registry);
            resolver
                .UseExecutionCommandingAuditor<LoggingCommandExecutionAuditor>()
                .UseAuditItemEnricher<AuditItemUserIdEnricher>();

            string requestBody = new StreamReader(req.Body).ReadToEnd();
            GetStoreProductQuery query = JsonConvert.DeserializeObject<GetStoreProductQuery>(requestBody);
            


            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
