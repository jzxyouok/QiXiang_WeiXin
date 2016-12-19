using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QMP.ViewModels
{
  public   class OfficialAccounts_ViewModel
    {
        public System.Guid AccountID { get; set; }
        public string WeiXinName { get; set; }
        public string OriginalID { get; set; }
        public string WeiXinNumber { get; set; }
        public string AppID { get; set; }
        public string AppSecret { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public string WebSiteTitle { get; set; }
    }
}
