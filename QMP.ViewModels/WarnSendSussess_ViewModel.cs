using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QMP.ViewModels
{
    public class WarnSendSussess_ViewModel
    {
        public string Title { get; set; }
        public Nullable<int> SuccessCount { get; set; }
        public Nullable<int> FailedCount { get; set; }
        public Nullable<int> TotalCount { get; set; }
    }
}
