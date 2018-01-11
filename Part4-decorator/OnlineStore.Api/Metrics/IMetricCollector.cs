namespace OnlineStore.Api.Metrics
{
    internal interface IMetricCollector
    {
        void Complete();
        void CompleteWithError();
    }
}
