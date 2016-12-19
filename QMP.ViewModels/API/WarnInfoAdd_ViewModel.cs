using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using QMP.Models;

namespace QMP.ViewModels.API
{
    public class WarnInfoAdd_ViewModel
    {
        
        [Display(Name = "内容")]
        public string Content { get; set; }
        [Display(Name = "防御")]
        public string FangYu
        { get; set; }
        [Display(Name = "发布时间")]
        public DateTime ReleaseTime { get; set; }
       
        public Nullable<System.Guid> UserID { get; set; }
        public Nullable<System.Guid> AccountID { get; set; }
        public string WarningCategory { get; set; }
        public string WarningLevel { get; set; }

       
    }
}
