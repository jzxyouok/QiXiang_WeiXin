using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QMP.ViewModels.API
{
   public  class ServiceInfoAdd_ViewModel
    {
      
        public string Title { get; set; }
       
        public string Content { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<System.Guid> AccountID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }

       
    }
}
