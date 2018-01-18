namespace OnlineStore.Api.Metrics
{
    internal interface IMetricCollector
    {
        void Record(string type, long elapsedMilliseconds);
        void RecordWithError(string type, long elapsedMilliseconds);
    }
}
