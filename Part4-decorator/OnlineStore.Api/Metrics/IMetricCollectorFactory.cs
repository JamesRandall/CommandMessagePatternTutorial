namespace OnlineStore.Api.Metrics
{
    interface IMetricCollectorFactory
    {
        IMetricCollector Create<T>();
    }
}
