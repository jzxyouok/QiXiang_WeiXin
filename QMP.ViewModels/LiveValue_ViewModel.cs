using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QMP.ViewModels
{
    public class LiveValue_ViewModel
    {
        public string StaName { get; set; }
        public float Value { get; set; }
        public DateTime DateTime { get; set; }
        public string DateString { get; set; }
        public float Lon { get; set; }
        public float Lat { get; set; }
        public string Country { get; set; }
       
        
    }
    public class RainValue_ViewModel
    {
        public string StaName { get; set; }
        public Nullable<double> Value { get; set; }




    }

    public class LiveValue_All_ViewModel
    {
        public string STANAME { get; set; }
        public Nullable<double> TEMPERATURE { get; set; }
        public Nullable<double> DWSPEED { get; set; }
        public Nullable<double> DWDIRECTION { get; set; }
        public Nullable<double> VISIBILITY { get; set; }
        public Nullable<double> HRAIN { get; set; }
        public Nullable<DateTime> FDATE { get; set; }
        public Nullable<double> LAT { get; set; }
        public Nullable<double> LON { get; set; }
        public string QXNAME { get; set; }
    }

    public class LiveValue_All_ViewModel_Result
    {
        public string DateTime { get; set; }

        public List<LiveValue_All_ViewModel> DataList { get; set; }
    }
}
