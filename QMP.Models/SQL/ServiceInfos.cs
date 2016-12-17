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
    
    public partial class ServiceInfos
    {
        public ServiceInfos()
        {
            this.ServiceInfos_Images = new HashSet<ServiceInfos_Images>();
        }
    
        public System.Guid ServiceID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> ReleaseTime { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<System.Guid> AccountID { get; set; }
        public Nullable<System.Guid> UserID { get; set; }
    
        public virtual OfficialAccounts OfficialAccounts { get; set; }
        public virtual ICollection<ServiceInfos_Images> ServiceInfos_Images { get; set; }
        public virtual ServiceInfos_Categorys ServiceInfos_Categorys { get; set; }
        public virtual Users Users { get; set; }
    }
}
