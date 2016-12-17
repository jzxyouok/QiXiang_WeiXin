using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using QMP.Models.SQL;
using QMP.Utility;

namespace QMP.BLL.SQL
{
    public partial class Users_BLL
    {
        public bool CheckExist(string username)
        {

            return dal.GetCount(a => a.UserName == username) > 0 ? true : false;
        }
        public bool CheckExist(string username, string password)
        {
            password = SecurityHelper.MD5(password);
            return dal.GetCount(a => a.UserName == username && a.Password == password) > 0 ? true : false;
        }
        public Users GetCurrentUser()
        {

            Users user = dal.Get(a => a.UserName == HttpContext.Current.User.Identity.Name);
            return user;
        }

        public int Add(Users model)
        {
            model.Password = SecurityHelper.MD5(model.Password);
            return dal.Add(model);
        }
    }
}