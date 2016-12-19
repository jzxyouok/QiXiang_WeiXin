using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using QMP.Models;
using QMP.Models.SQL;

namespace QMP.ViewModels
{
   public  class WeatherInfos_ViewModel
    {
     
        public System.Guid InfoID { get; set; }

        [Display(Name = "内容")]
        [Required(ErrorMessage = "请输入内容")]
        public string Content { get; set; }
        [Display(Name = "发布时间")]
        public Nullable<System.DateTime> CreateTime { get; set; }
        [Display(Name = "发布人")]
        public Nullable<System.Guid> UserID { get; set; }
     
        public Nullable<System.Guid> AccountID { get; set; }

        [Display(Name = "预报类型")]
        [Required(ErrorMessage = "请输入预报类型")]
        public Nullable<int> CategoryID { get; set; }

        public virtual OfficialAccounts OfficialAccounts { get; set; }
        public virtual Users Users { get; set; }
        public virtual WeatherInfos_Categorys WeatherInfos_Categorys { get; set; }
    }
}
