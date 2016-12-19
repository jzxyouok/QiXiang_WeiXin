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

    public class OfficialAccountsController : Controller
    {
        //
        // GET: /Admin/OfficialAccounts/

        public ActionResult Add()
        {
            return View();
        }

         
        [HttpPost]
        public ActionResult Add(OfficialAccounts_ViewModel model)
        {

            AutoMapper.Mapper.CreateMap<OfficialAccounts_ViewModel, OfficialAccounts>();
            OfficialAccounts newmodel = AutoMapper.Mapper.Map<OfficialAccounts>(model);

            newmodel.AccountID = Guid.NewGuid();
            newmodel.CreateTime = DateTime.Now;
         

            OfficialAccounts_BLL bll = new OfficialAccounts_BLL();
            if (bll.Add(newmodel) > 0)
            {
                return RedirectToAction("Add");
            }
            else
            {
                ModelState.AddModelError("", "添加失败，请稍后再试！");
                return View(model);
            }
        }
    }
}
