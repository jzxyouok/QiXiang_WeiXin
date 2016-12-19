using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace QMP.ViewModels
{
   public  class WarnInfos_Relieve_ViewModel
    {
       public Guid InfoID { get; set; }

       public string Category { get; set; }
       public string Level { get; set; }
       [Display(Name = "内容")]
       [Required(ErrorMessage = "请输入内容")]
       public string Content { get; set; }
       [Display(Name = "解除时间")]
       [Required(ErrorMessage = "请输入解除时间")]
       public DateTime RelieveTime { get; set; }
       
    }
}
