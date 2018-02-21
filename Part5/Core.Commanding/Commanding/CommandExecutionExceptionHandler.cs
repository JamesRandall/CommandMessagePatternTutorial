using System;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Commanding.Exceptions;

namespace Core.Commanding.Commanding
{
    public class CommandExecutionExceptionHandler : ICommandExecutionExceptionHandler
    {
        public Task<bool> HandleException<TResult>(Exception ex, object handler, int handlerExecutionIndex, ICommand<TResult> command,
            ICommandDispatchContext dispatchContext)
        {
            throw new DispatcherException($"Error occurred performing operation {command.GetType().Name}", ex);
        }
    }
}
