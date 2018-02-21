namespace Core.Commanding.Metrics
{
    public class MetricCollector : IMetricCollector
    {
        
        public MetricCollector()
        {
            //_telemetryClient = telemetryClient;
        }

        public void Record(string type, long elapsedMilliseconds)
        {
            //_telemetryClient.TrackMetric(type, elapsedMilliseconds, new Dictionary<string, string>{ {"success", "true"} });
        }

        public void RecordWithError(string type, long elapsedMilliseconds)
        {
            //_telemetryClient.TrackMetric(type, elapsedMilliseconds, new Dictionary<string, string> { { "success", "false" } });
        }
    }
}
