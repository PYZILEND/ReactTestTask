using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactTestTask.ViewModels
{
    public class MetricsViewModel
    {
        public int RollingRetention { get; set; }
        public int[] LifetiemeDistribution { get; set; }
    }
}