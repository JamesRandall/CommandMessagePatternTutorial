using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AzureFromTheTrenches.Commanding.Abstractions;
using AzureFromTheTrenches.Commanding.Abstractions.Model;
using Core.Model;
using Microsoft.Extensions.Logging;
using Moq;
using OnlineStore.Api.Commanding;
using OnlineStore.Api.Exceptions;
using OnlineStore.Api.Metrics;
using OnlineStore.Api.Tests.TestAssets;
using Xunit;

namespace OnlineStore.Api.Tests.Commanding
{
    public class LoggingCommandDispatcherShould
    {
        [Fact]
        public async Task LogBeforeAndAfterSuccessfulCommand()
        {
            Mock<IFrameworkCommandDispatcher> frameworkCommandDispatcher = new Mock<IFrameworkCommandDispatcher>();
            TestLogger logger = new TestLogger();
            Mock<IMetricCollectorFactory> metricCollectorFactory = new Mock<IMetricCollectorFactory>();
            Mock<IMetricCollector> metricCollector = new Mock<IMetricCollector>();
            metricCollectorFactory.Setup(x => x.Create(It.IsAny<Type>())).Returns(metricCollector.Object);
            SimpleCommandCommandResponseResult command = new SimpleCommandCommandResponseResult();
            LoggingCommandDispatcher testSubject = new LoggingCommandDispatcher(
                frameworkCommandDispatcher.Object,
                logger,
                metricCollectorFactory.Object
                );

            await testSubject.DispatchAsync(command);

            Assert.Equal("Executing command SimpleCommandCommandResponseResult", logger.Messages[0]);
            Assert.Equal("Successfully executed command SimpleCommandCommandResponseResult", logger.Messages[1]);
            frameworkCommandDispatcher.Verify(x => x.DispatchAsync(command, It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task LogAndReturnErrorResult()
        {
            Mock<IFrameworkCommandDispatcher> frameworkCommandDispatcher = new Mock<IFrameworkCommandDispatcher>();
            TestLogger logger = new TestLogger();
            Mock<IMetricCollectorFactory> metricCollectorFactory = new Mock<IMetricCollectorFactory>();
            Mock<IMetricCollector> metricCollector = new Mock<IMetricCollector>();
            metricCollectorFactory.Setup(x => x.Create(It.IsAny<Type>())).Returns(metricCollector.Object);
            SimpleCommandCommandResponseResult command = new SimpleCommandCommandResponseResult();
            frameworkCommandDispatcher.Setup(x => x.DispatchAsync(command, It.IsAny<CancellationToken>()))
                .Throws(new Exception());
            LoggingCommandDispatcher testSubject = new LoggingCommandDispatcher(
                frameworkCommandDispatcher.Object,
                logger,
                metricCollectorFactory.Object
            );

            CommandResult<CommandResponse<bool>> result;
            await Assert.ThrowsAsync<DispatcherException>(async () => result = await testSubject.DispatchAsync(command));
            
            Assert.Equal("Executing command SimpleCommandCommandResponseResult", logger.Messages[0]);
            Assert.Equal("Error executing command SimpleCommandCommandResponseResult", logger.Messages[1]);
        }
    }
}
