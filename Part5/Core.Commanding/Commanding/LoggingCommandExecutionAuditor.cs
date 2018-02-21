using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.Abstractions.Model;
using Microsoft.Extensions.Logging;
using OnlineStore.Api.Metrics;

namespace OnlineStore.Api.Commanding
{
    public class LoggingCommandExecutionAuditor : ICommandAuditor
    {
        private readonly ILogger _logger;
        private readonly IMetricCollector _metricCollector;

        /*public LoggingCommandExecutionAuditor(ILogger logger,
            IMetricCollector metricCollector)
        {
            _logger = logger;
            _metricCollector = metricCollector;
        }*/

        public Task Audit(AuditItem auditItem, CancellationToken cancellationToken)
        {
            /*Debug.Assert(auditItem.ExecutedSuccessfully.HasValue);
            Debug.Assert(auditItem.ExecutionTimeMs.HasValue);

            if (auditItem.ExecutedSuccessfully.Value)
            {
                if (auditItem.AdditionalProperties.ContainsKey("UserId"))
                {
                    _logger.LogInformation("Successfully executed command {commandType} for user {userId}",
                        auditItem.CommandType,
                        auditItem.AdditionalProperties["UserId"]);
                }
                else
                {
                    _logger.LogInformation("Executing command {commandType}",
                        auditItem.CommandType);
                }
                _metricCollector.Record(auditItem.CommandType, auditItem.ExecutionTimeMs.Value);
            }
            else
            {
                if (auditItem.AdditionalProperties.ContainsKey("UserId"))
                {
                    _logger.LogInformation("Error executing command {commandType} for user {userId}",
                        auditItem.CommandType,
                        auditItem.AdditionalProperties["UserId"]);
                }
                else
                {
                    _logger.LogInformation("Error executing command {commandType}",
                        auditItem.CommandType);
                }
                _metricCollector.RecordWithError(auditItem.CommandType, auditItem.ExecutionTimeMs.Value);
            }*/
            
            return Task.FromResult(0);
        }
    }
}
