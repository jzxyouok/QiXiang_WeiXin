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
    
    public partial class Users_Roles
    {
        public Users_Roles()
        {
            this.Users = new HashSet<Users>();
        }
    
        public System.Guid RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
    
        public virtual ICollection<Users> Users { get; set; }
    }
}