using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QMP.Models;
using QMP.Models.SQL;

namespace QMP.ViewModels
{
    public class Messages_ViewModel
    {
        public System.Guid MsgID { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> SendTime { get; set; }
        public Nullable<int> SendTotalCount { get; set; }
        public Nullable<int> SendSuccessCount { get; set; }
        public string SendSuccessPercent { get; set; }
        public string SendFailedPercent { get; set; }
        public Nullable<int> SendFailedCount { get; set; }
        public string Title { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public Nullable<System.Guid> AccountID { get; set; }
        public Nullable<int> CategoryID { get; set; }

        public virtual Messages_Categorys Messages_Categorys { get; set; }
        public virtual OfficialAccounts OfficialAccounts { get; set; }
        public virtual Users Users { get; set; }
    }
}
