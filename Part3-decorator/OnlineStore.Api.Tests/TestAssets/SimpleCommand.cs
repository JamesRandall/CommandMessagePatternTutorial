using AzureFromTheTrenches.Commanding.Abstractions;
using Core.Model;

namespace OnlineStore.Api.Tests.TestAssets
{
    internal class SimpleCommandNoResult : ICommand
    {
    }

    internal class SimpleCommandWithResult : ICommand<bool>
    {
        
    }

    internal class SimpleCommandCommandResponse : ICommand<CommandResponse>
    {
        
    }
}
