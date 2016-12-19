using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using QMP.Models;

namespace QMP.ViewModels
{
     public  class WarnInfos_ViewModel
    {
        public System.Guid InfoID { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "防御")]
        public string FangYu
 { get; set; }
        [Display(Name = "发布时间")]
        public Nullable<System.DateTime> ReleaseTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public Nullable<System.Guid> AccountID { get; set; }
        public string WarningCategory { get; set; }
        public string WarningLevel { get; set; }

        public virtual OfficialAccounts OfficialAccounts { get; set; }
        public virtual Users Users { get; set; }
    }

   
}
