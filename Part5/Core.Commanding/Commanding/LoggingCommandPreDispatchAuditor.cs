using System.Threading;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.Abstractions.Model;
using Microsoft.Extensions.Logging;

namespace OnlineStore.Api.Commanding
{
    internal class LoggingCommandPreDispatchAuditor : ICommandAuditor
    {
        private readonly ILogger<LoggingCommandPreDispatchAuditor> _logger;

        public LoggingCommandPreDispatchAuditor(ILogger<LoggingCommandPreDispatchAuditor> logger)
        {
            _logger = logger;
        }

        public Task Audit(AuditItem auditItem, CancellationToken cancellationToken)
        {
            if (auditItem.AdditionalProperties.ContainsKey("UserId"))
            {
                _logger.LogInformation("Executing command {commandType} for user {userId}",
                    auditItem.CommandType,
                    auditItem.AdditionalProperties["UserId"]);
            }
            else
            {
                _logger.LogInformation("Executing command {commandType}",
                    auditItem.CommandType);
            }
            return Task.FromResult(0);
        }
    }
}
