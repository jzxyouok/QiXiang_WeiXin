using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QMP.ViewModels.API
{
    public class WeatherInfoAdd_ViewModel
    {
     
        public string Content { get; set; }
       
        public Nullable<System.DateTime> CreateTime { get; set; }
       
        public Nullable<System.Guid> UserID { get; set; }

        public Nullable<System.Guid> AccountID { get; set; }

       
        public Nullable<int> CategoryID { get; set; }

     
    }
}
