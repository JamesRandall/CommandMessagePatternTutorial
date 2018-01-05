using System;
using System.Diagnostics;

namespace OnlineStore.Api.Metrics
{
    public class MetricCollector<TType> : IMetricCollector
    {
        private readonly Stopwatch _stopwatch;

        public MetricCollector()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public void Complete()
        {
            _stopwatch.Stop();
            
        }
    }
}
