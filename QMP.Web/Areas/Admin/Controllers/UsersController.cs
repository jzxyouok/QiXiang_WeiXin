using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMP.BLL;
using QMP.Models;
using QMP.ViewModels;


namespace QMP.Web.Areas.Admin.Controllers
{
    [Authorize]

    public class UsersController : Controller
    {
        //
        // GET: /Admin/Users/

        public ActionResult Add()
        {
            ViewBag.RolesList = new SelectList(new Users_Roles_BLL().GetList(), "RoleID", "RoleName", null);
            ViewBag.AccountsList = new SelectList(new OfficialAccounts_BLL().GetList(), "AccountID", "WeiXinName", null);
              return View();
        }

        [HttpPost]
        public ActionResult Add(Users_Add_ViewModel model)
        {
            AutoMapper.Mapper.CreateMap<Users_Add_ViewModel, Users>();
            Users newmodel = AutoMapper.Mapper.Map<Users>(model);

            newmodel.UserID = Guid.NewGuid();
            newmodel.CreateTime = DateTime.Now;
            


            Users_BLL bll = new Users_BLL();
            if (bll.Add(newmodel) > 0)
            {
                return RedirectToAction("Add");
            }
            else
            {
                ViewBag.RolesList = new SelectList(new Users_Roles_BLL().GetList(), "RoleID", "RoleName", null);
                ViewBag.AccountsList = new SelectList(new OfficialAccounts_BLL().GetList(), "AccountID", "WeiXinName", null);
        
                ModelState.AddModelError("", "添加失败，请稍后再试！");
                return View(model);
            }
        }

    }
}
