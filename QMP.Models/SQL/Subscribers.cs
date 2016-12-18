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
    
    public partial class Subscribers
    {
        public System.Guid SubscribeID { get; set; }
        public string NickName { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public Nullable<int> Sex { get; set; }
        public string HeadImgUrl { get; set; }
        public Nullable<System.DateTime> SubscribeTime { get; set; }
        public Nullable<double> Longitude { get; set; }
        public Nullable<double> Latitude { get; set; }
        public Nullable<double> Precision { get; set; }
        public Nullable<System.DateTime> LocationTime { get; set; }
        public string OpenID { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<System.Guid> AccountID { get; set; }
        public Nullable<System.Guid> ReceivedLastSanDayID { get; set; }
        public Nullable<System.Guid> ReceivedLastShortNearID { get; set; }
        public Nullable<System.DateTime> ReceivedLastSanDayTime { get; set; }
        public Nullable<System.DateTime> ReceivedLastShortNearTime { get; set; }
    
        public virtual OfficialAccounts OfficialAccounts { get; set; }
    }
}