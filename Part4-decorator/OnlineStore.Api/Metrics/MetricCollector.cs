using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ApplicationInsights;

namespace OnlineStore.Api.Metrics
{
    public class MetricCollector : IMetricCollector
    {
        private readonly string _metricName;
        private readonly TelemetryClient _telemetryClient;
        private readonly Stopwatch _stopwatch;

        public MetricCollector(string metricName, TelemetryClient telemetryClient)
        {
            _metricName = metricName;
            _telemetryClient = telemetryClient;
            _stopwatch = Stopwatch.StartNew();
        }

        public void Complete()
        {
            _stopwatch.Stop();
            _telemetryClient.TrackMetric(_metricName, _stopwatch.ElapsedMilliseconds, new Dictionary<string, string>{ {"success", "true"} });
        }

        public void CompleteWithError()
        {
            _stopwatch.Stop();
            _telemetryClient.TrackMetric(_metricName, _stopwatch.ElapsedMilliseconds, new Dictionary<string, string> { { "success", "false" } });
        }
    }
}
