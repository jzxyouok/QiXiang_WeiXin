//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace QMP.Models.SQL
{
    using System;
    using System.Collections.Generic;
    
    public partial class WarnInfos
    {
        public System.Guid InfoID { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> ReleaseTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
        public Nullable<System.Guid> AccountID { get; set; }
        public string WarningCategory { get; set; }
        public string WarningLevel { get; set; }
        public string ImageName { get; set; }
        public Nullable<bool> IsTest { get; set; }
    
        public virtual OfficialAccounts OfficialAccounts { get; set; }
        public virtual Users Users { get; set; }
    }
}
