using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QMP.ViewModels
{
    public class Wind_ViewModel
    {
        public string StaName { get; set; }
        public float WindDirection { get; set; }
        public float WindSpeed { get; set; }
        public DateTime DateTime { get; set; }
        public string DateString { get; set; }
        public float Lon { get; set; }
        public float Lat { get; set; }
        public string Country { get; set; }
    }
}
