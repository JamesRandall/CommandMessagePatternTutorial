using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStore.Api.Metrics
{
    internal interface IMetricCollector
    {
        void Complete();
    }
}
