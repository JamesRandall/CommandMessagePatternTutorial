using System;
using System.Threading;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.Abstractions.Model;
using Core.Model;
using Microsoft.Extensions.Logging;

namespace OnlineStore.Api.Commanding
{
    public class LoggingCommandDispatcher : ICommandDispatcher
    {
        private readonly IFrameworkCommandDispatcher _underlyingDispatcher;
        private readonly ILogger<LoggingCommandDispatcher> _logger;

        public LoggingCommandDispatcher(IFrameworkCommandDispatcher underlyingDispatcher,
            ILoggerFactory loggerFactory)
        {
            _underlyingDispatcher = underlyingDispatcher;
            _logger = loggerFactory.CreateLogger<LoggingCommandDispatcher>();
        }

        public async Task<CommandResult<TResult>> DispatchAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken)
        {
            try
            {
                LogPreDispatchMessage(command);
                CommandResult<TResult> result = await _underlyingDispatcher.DispatchAsync(command, cancellationToken);
                LogSuccessfulPostDispatchMessage(command);
                return result;
            }
            catch (Exception ex)
            {
                LogFailedPostDispatchMessage(command, ex);
                throw;
            }
        }

        public async Task<CommandResult> DispatchAsync(ICommand command, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                LogPreDispatchMessage(command);
                CommandResult result = await _underlyingDispatcher.DispatchAsync(command, cancellationToken);
                LogSuccessfulPostDispatchMessage(command);
                return result;
            }
            catch (Exception ex)
            {
                LogFailedPostDispatchMessage(command, ex);
                throw;
            }
        }

        public ICommandExecuter AssociatedExecuter => _underlyingDispatcher.AssociatedExecuter;

        private void LogPreDispatchMessage(ICommand command)
        {
            if (command is ILogAwareCommand logAwareCommand)
            {
                _logger.LogInformation(logAwareCommand.GetPreDispatchLogMessage());
            }
            else if (command is IUserContextCommand userContextCommand)
            {
                _logger.LogInformation("Executing command {commandType} for user {userId}", command.GetType().Name,
                    userContextCommand.AuthenticatedUserId);
            }
            else
            {
                _logger.LogInformation("Executing command {commandType}", command.GetType().Name);
            }
        }

        private void LogSuccessfulPostDispatchMessage(ICommand command)
        {
            if (command is ILogAwareCommand logAwareCommand)
            {
                _logger.LogInformation(logAwareCommand.GetPreDispatchLogMessage());
            }
            else if (command is IUserContextCommand userContextCommand)
            {
                _logger.LogInformation("Successfully executed command {commandType} for user {userId}", command.GetType().Name,
                    userContextCommand.AuthenticatedUserId);
            }
            else
            {
                _logger.LogInformation("Successfully executed command {commandType}", command.GetType().Name);
            }
        }

        private void LogFailedPostDispatchMessage(ICommand command, Exception ex)
        {
            string message = null;
            if (command is ILogAwareCommand logAwareCommand)
            {
                message = logAwareCommand.GetDispatchErrorLogMessage(ex);
            }
            if (message == null && command is IUserContextCommand userContextCommand)
            {
                _logger.LogError(ex, "Error executing command {commandType} for user {userId}", command.GetType().Name,
                    userContextCommand.AuthenticatedUserId);
            }
            else
            {
                _logger.LogError(ex, "Error executing command {commandType}", command.GetType().Name);
            }
        }
    }
}
