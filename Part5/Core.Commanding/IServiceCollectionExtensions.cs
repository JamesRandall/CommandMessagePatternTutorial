using AzureFromTheTrenches.Commanding;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Commanding.Commanding;
using Core.Commanding.Metrics;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Commanding
{
    // ReSharper disable once InconsistentNaming - interface extensions
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection UseCoreCommanding(this IServiceCollection serviceCollection,
            ICommandingDependencyResolver commandingDependencyResolver)
        {
            serviceCollection.AddSingleton<IMetricCollector>(new MetricCollector());
            commandingDependencyResolver
                .UsePreDispatchCommandingAuditor<LoggingCommandPreDispatchAuditor>()
                .UseExecutionCommandingAuditor<LoggingCommandExecutionAuditor>()
                .UseAuditItemEnricher<AuditItemUserIdEnricher>();
            return serviceCollection;
        }
    }
}
