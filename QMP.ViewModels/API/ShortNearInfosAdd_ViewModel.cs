using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QMP.ViewModels.API
{
    public class ShortNearInfosAdd_ViewModel
    {

    
        public string Content { get; set; }
  

        public Nullable<System.DateTime> StartTime { get; set; }
   
        public Nullable<System.DateTime> EndTime { get; set; }

        public Nullable<int> DuringHour { get; set; }
     
        public Nullable<System.Guid> UserID { get; set; }
   
        public Nullable<System.Guid> AccountID { get; set; }
    
    

    }
}
