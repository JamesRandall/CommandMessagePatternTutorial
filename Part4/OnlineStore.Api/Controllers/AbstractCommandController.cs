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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TResult response = await Dispatcher.DispatchAsync(command);
            return Ok(response);
        }

        protected async Task<IActionResult> ExecuteCommand(ICommand<CommandResponse> command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CommandResponse response = await Dispatcher.DispatchAsync(command);
            if (response.IsSuccess)
            {
                return Ok();
            }
            response.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        protected async Task<IActionResult> ExecuteCommand<TResult>(ICommand<CommandResponse<TResult>> command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CommandResponse<TResult> response = await Dispatcher.DispatchAsync(command);
            if (response.IsSuccess)
            {
                return Ok(response.Result);
            }
            response.AddToModelState(ModelState);
            return BadRequest(ModelState);
        }

        protected async Task<IActionResult> ExecuteCommand<TCommand>() where TCommand : class, ICommand<CommandResponse>, new()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TCommand command = CreateCommand<TCommand>();
            return await ExecuteCommand(command);
        }

        protected async Task<IActionResult> ExecuteCommand<TCommand, TResult>() where TCommand : class, ICommand<CommandResponse<TResult>>, new()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TCommand command = CreateCommand<TCommand>();
            CommandResponse<TResult> response = await Dispatcher.DispatchAsync(command);
            if (response.IsSuccess)
            {
                return Ok(response.Result);
            }
            response.AddToModelState(ModelState);
            return BadRequest(ModelState);
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
