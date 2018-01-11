using System;

namespace OnlineStore.Api.Metrics
{
    interface IMetricCollectorFactory
    {
        IMetricCollector Create<T>();
        IMetricCollector Create(Type type);
    }
}
