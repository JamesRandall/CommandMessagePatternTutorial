using System.Threading;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.Abstractions.Model;
using Core.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using OnlineStore.Api.Controllers;
using OnlineStore.Api.Tests.TestAssets;
using Xunit;

namespace OnlineStore.Api.Tests.Controllers
{
    public class AbstractCommandControllerShould
    {
        // To test an abstract class with protected members we inherit from it
        // and generate some proxy pass through methods
        private class TestSubjectController : AbstractCommandController
        {
            public TestSubjectController(ICommandDispatcher dispatcher) : base(dispatcher)
            {
            }

            public Task<IActionResult> ExecuteCommandProxy<TResult>(ICommand<TResult> command) => ExecuteCommand(command);

            public Task<IActionResult> ExecuteCommandProxy(ICommand<CommandResponse> command) => ExecuteCommand(command);

            public Task<IActionResult> ExecuteCommandProxy<TResult>(ICommand<CommandResponse<TResult>> command) => ExecuteCommand(command);

            public Task<IActionResult> ExecuteCommandProxy<TCommand>() where TCommand : class, ICommand<CommandResponse>, new() => ExecuteCommand<TCommand>();

            public Task<IActionResult> ExecuteCommandProxy<TCommand, TResult>() where TCommand : class, ICommand<CommandResponse<TResult>>, new() => ExecuteCommand<TCommand, TResult>();
        }

        [Fact]
        public async Task ExecuteCommandWithResultGeneratesOkResponse()
        {
            SimpleCommandWithResult command = new SimpleCommandWithResult();
            Mock<ICommandDispatcher> dispatcher = new Mock<ICommandDispatcher>();
            dispatcher.Setup(x => x.DispatchAsync(command, It.IsAny<CancellationToken>())).ReturnsAsync(new CommandResult<bool>(true, false));
            TestSubjectController controller = new TestSubjectController(dispatcher.Object);

            IActionResult result = await controller.ExecuteCommandProxy(command);

            OkObjectResult castResult = (OkObjectResult) result;
            Assert.True((bool)castResult.Value);
            Assert.Equal(200, castResult.StatusCode);
        }

        [Fact]
        public async Task ExecuteCommandWithCommandResponseNoResultGeneratesOkResponse()
        {
            SimpleCommandCommandResponse command = new SimpleCommandCommandResponse();
            Mock<ICommandDispatcher> dispatcher = new Mock<ICommandDispatcher>();
            dispatcher.Setup(x => x.DispatchAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CommandResult<CommandResponse>(CommandResponse.Ok(), false));
            TestSubjectController controller = new TestSubjectController(dispatcher.Object);

            IActionResult result = await controller.ExecuteCommandProxy(command);

            OkResult castResult = (OkResult)result;
            Assert.Equal(200, castResult.StatusCode);
        }

        [Fact]
        public async Task ExecuteCommandWithCommandResponseNoResultGeneratesBadResponse()
        {
            SimpleCommandCommandResponse command = new SimpleCommandCommandResponse();
            Mock<ICommandDispatcher> dispatcher = new Mock<ICommandDispatcher>();
            dispatcher.Setup(x => x.DispatchAsync(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new CommandResult<CommandResponse>(CommandResponse.WithError("went wrong"), false));
            TestSubjectController controller = new TestSubjectController(dispatcher.Object);

            IActionResult result = await controller.ExecuteCommandProxy(command);

            BadRequestObjectResult castResult = (BadRequestObjectResult)result;
            Assert.Equal(400, castResult.StatusCode);
            Assert.Equal("went wrong", castResult.Value);
        }
    }
}
