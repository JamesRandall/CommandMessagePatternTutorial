namespace OnlineStore.Api.Metrics
{
    internal class MetricCollectorFactory : IMetricCollectorFactory
    {
        public IMetricCollector Create<TType>()
        {
            return new MetricCollector<TType>();
        }
    }
}
