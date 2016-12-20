using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QMP.ViewModels
{
    public class HistoryChartValue_ViewModel
    {
        public string StaName { get; set; }
        public DateTime FDate { get; set; }
        public string HourString { get; set; }
        public string DateString { get; set; }
        public Nullable<decimal> Rain { get; set; }
        public Nullable<decimal> WindSpeed { get; set; }
        public Nullable<decimal> Temperature { get; set; }

    }
}
