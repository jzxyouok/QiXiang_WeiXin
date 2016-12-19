using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using QMP.Models;

namespace QMP.ViewModels
{
   public class ShortNearInfos_ViewModel
    {
        public System.Guid InfoID { get; set; }
        [Display(Name = "预报内容")]
        [Required(ErrorMessage = "请输入预报内容")]

        public string Content { get; set; }
        [Display(Name = "预报时间")]
        [Required(ErrorMessage = "请输入预报时间")]

        public Nullable<System.DateTime> StartTime { get; set; }
        [Display(Name = "结束时间")]
        public Nullable<System.DateTime> EndTime { get; set; }
        [Display(Name = "预报时限")]
        [Required(ErrorMessage = "请输入预报时限")]

        public Nullable<int> DuringHour { get; set; }
        [Display(Name = "发布人")]
        public Nullable<System.Guid> UserID { get; set; }
        [Display(Name = "微信公众号")]
        public Nullable<System.Guid> AccountID { get; set; }
        [Display(Name = "创建时间")]
        public Nullable<System.DateTime> CreateTime { get; set; }

        public virtual OfficialAccounts OfficialAccounts { get; set; }
        public virtual Users Users { get; set; }
    }
}
