using System;
using Microsoft.ApplicationInsights;

namespace OnlineStore.Api.Metrics
{
    internal class MetricCollectorFactory : IMetricCollectorFactory
    {
        private readonly TelemetryClient _telemetryClient;

        public MetricCollectorFactory(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public IMetricCollector Create<TType>()
        {
            return new MetricCollector(typeof(TType).Name, _telemetryClient);
        }

        public IMetricCollector Create(Type type)
        {
            return new MetricCollector(type.Name, _telemetryClient);
        }
    }
}
