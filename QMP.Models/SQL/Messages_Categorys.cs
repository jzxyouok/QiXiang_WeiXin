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
    
    public partial class Messages_Categorys
    {
        public Messages_Categorys()
        {
            this.Messages = new HashSet<Messages>();
        }
    
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public int CategoryID { get; set; }
    
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
