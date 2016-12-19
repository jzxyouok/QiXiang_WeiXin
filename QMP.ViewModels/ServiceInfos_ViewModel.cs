using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using QMP.Models;
using QMP.Models.SQL;

namespace QMP.ViewModels
{
    public class ServiceInfos_ViewModel
    {
        public System.Guid ServiceID { get; set; }
        [Display(Name = "标题")]
        public string Title { get; set; }
        [Display(Name = "内容")]
        public string Content { get; set; }
        public Nullable<System.DateTime> ReleaseTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        [Display(Name = "服务类型")]
        
        public Nullable<int> CategoryID { get; set; }
        public Nullable<System.Guid> AccountID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }

        public virtual OfficialAccounts OfficialAccounts { get; set; }
        public virtual ServiceInfos_Categorys ServiceInfos_Categorys { get; set; }
        public virtual Users Users { get; set; }
        public virtual ICollection<ServiceInfos_Images> ServiceInfos_Images { get; set; }
    }
}
