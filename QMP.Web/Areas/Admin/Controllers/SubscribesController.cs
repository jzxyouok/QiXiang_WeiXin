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
    public class SubscribesController : Controller
    {
        //
        // GET: /Admin/Subscribes/

        public ActionResult MapList()
        {
            Users user = new Users_BLL().GetCurrentUser();
            ViewBag.AccountId = user.AccountID;
            return View();
        }
        public JsonResult GetSubscribesList(Guid accountid)
        {
            Subscribers_BLL bll = new Subscribers_BLL();
            List<Subscribers> list = bll.GetList(a => a.AccountID == accountid && a.Longitude != null && a.Latitude != null).OrderByDescending(a => a.SubscribeTime).ToList();


            AutoMapper.Mapper.CreateMap<Subscribers, Subscribers_Map_ViewModel>();
            List<Subscribers_Map_ViewModel> vlist = AutoMapper.Mapper.Map<List<Subscribers_Map_ViewModel>>(list);


            foreach (var item in vlist)
            {
                item.SubscribeTimeString = ((DateTime)item.SubscribeTime).ToString("yyyy-MM-dd HH:mm");
              
            }
            

            return Json(vlist, JsonRequestBehavior.AllowGet);
        }



    }
}
