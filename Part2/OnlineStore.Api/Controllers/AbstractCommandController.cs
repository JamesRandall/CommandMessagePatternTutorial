using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Api.Extensions;

namespace OnlineStore.Api.Controllers
{
    public abstract class AbstractCommandController : Controller
    {
        protected AbstractCommandController(ICommandDispatcher dispatcher)
        {
            Dispatcher = dispatcher;
        }

        protected ICommandDispatcher Dispatcher { get; }

        protected async Task<IActionResult> ExecuteCommand<TResult>(ICommand<TResult> command)
        {
            TResult response = await Dispatcher.DispatchAsync(command);
            return Ok(response);
        }

        protected async Task<IActionResult> ExecuteCommand(ICommand<CommandResponse> command)
        {
            CommandResponse response = await Dispatcher.DispatchAsync(command);
            if (response.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(response.ErrorMessage);
        }

        protected async Task<IActionResult> ExecuteCommand<TResult>(ICommand<CommandResponse<TResult>> command)
        {
            CommandResponse response = await Dispatcher.DispatchAsync(command);
            if (response.IsSuccess)
            {
                return Ok();
            }
            return BadRequest(response.ErrorMessage);
        }

        protected Task<IActionResult> ExecuteCommand<TCommand>() where TCommand : class, ICommand<CommandResponse>, new()
        {
            TCommand command = CreateCommand<TCommand>();
            return ExecuteCommand(command);
        }

        protected async Task<IActionResult> ExecuteCommand<TCommand, TResult>() where TCommand : class, ICommand<CommandResponse<TResult>>, new()
        {
            TCommand command = CreateCommand<TCommand>();
            CommandResponse<TResult> response = await Dispatcher.DispatchAsync(command);
            if (response.IsSuccess)
            {
                return Ok(response.Result);
            }
            return BadRequest(response.ErrorMessage);
        }

        private TCommand CreateCommand<TCommand>() where TCommand : class, new()
        {
            TCommand command = new TCommand();
            if (command is IUserContextCommand userContextCommand)
            {
                userContextCommand.AuthenticatedUserId = this.GetUserId();
            }
            return command;
        }
    }
}
