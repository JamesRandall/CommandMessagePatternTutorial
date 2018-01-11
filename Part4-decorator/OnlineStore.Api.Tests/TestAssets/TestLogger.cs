using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using OnlineStore.Api.Commanding;

namespace OnlineStore.Api.Tests.TestAssets
{
    class TestLogger : ILogger<LoggingCommandDispatcher>
    {
        public List<string> Messages = new List<string>();

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            Messages.Add(formatter(state, exception));
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
