using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QMP.ViewModels.Emergency
{
   public class Emergency_Station_Data_ViewModel
    {
        public string StationName { get; set; }
        public string StationNumber { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime DateTime { get; set; }
        public  Nullable< decimal> WindSpeed { get; set; }
        public Nullable<decimal> WindDirection { get; set; }
        public Nullable<decimal> Temperature { get; set; }
        public Nullable<decimal> Rain { get; set; }

        public string Country { get; set; }

        public string EmergencyService { get; set; }

    }
}
