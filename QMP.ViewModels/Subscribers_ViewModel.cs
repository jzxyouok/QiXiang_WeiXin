using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QMP.ViewModels
{
     public class Subscribers_Map_ViewModel
    {
        public System.Guid SubscribeID { get; set; }
        public string NickName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Nullable<int> Sex { get; set; }
        public string HeadImgUrl { get; set; }
        public Nullable<System.DateTime> SubscribeTime { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Precision { get; set; }
        public Nullable<System.DateTime> LocationTime { get; set; }
        public string OpenID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.Guid> AccountID { get; set; }
        public string SubscribeTimeString { get; set; }

    }
}
