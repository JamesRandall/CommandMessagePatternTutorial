using System.Collections.Generic;
using Microsoft.ApplicationInsights;

namespace OnlineStore.Api.Metrics
{
    public class MetricCollector : IMetricCollector
    {
        private readonly TelemetryClient _telemetryClient;

        public MetricCollector(TelemetryClient telemetryClient)
        {
            _telemetryClient = telemetryClient;
        }

        public void Record(string type, long elapsedMilliseconds)
        {
            _telemetryClient.TrackMetric(type, elapsedMilliseconds, new Dictionary<string, string>{ {"success", "true"} });
        }

        public void RecordWithError(string type, long elapsedMilliseconds)
        {
            _telemetryClient.TrackMetric(type, elapsedMilliseconds, new Dictionary<string, string> { { "success", "false" } });
        }
    }
}
