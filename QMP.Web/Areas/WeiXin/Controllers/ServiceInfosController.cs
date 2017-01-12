using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QMP.BLL;
using QMP.BLL.SQL;
using QMP.Models;
using QMP.Models.SQL;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Helpers;

namespace QMP.Web.Areas.WeiXin.Controllers
{
    public class ServiceInfosController : Controller
    {
        //
        // GET: /WeiXin/ServiceInfos/

        public ActionResult List(Guid aid,string cname, int id = 1)
        {


            ServiceInfos_Categorys category = new ServiceInfos_Categorys_BLL().Get(a => a.AccountID == aid && a.CategoryName == cname);
            ViewBag.AccountId = aid;
            ViewBag.CategoryName = category.CategoryName;
            
            ServiceInfos_BLL bll = new ServiceInfos_BLL();
            List<ServiceInfos> ylist = bll.GetPageListOrderBy(id, 20, a => a.AccountID == aid && a.CategoryID == category.CategoryID, a => a.CreateTime, false).ToList();

            int totalCount = bll.GetCount(a => a.AccountID == aid && a.CategoryID == category.CategoryID);
            var list = new StaticPagedList<ServiceInfos>(ylist, id, 20, totalCount);
            return View(list);
        }
        public ActionResult Details(Guid id)
        {
            ServiceInfos_BLL bll = new ServiceInfos_BLL();

            ServiceInfos model = bll.Get(a => a.ServiceID == id);

            //var jssdkUiPackage = JSSDKHelper.GetJsSdkUiPackage(model.OfficialAccounts.AppID, model.OfficialAccounts.AppSecret, Request.Url.AbsoluteUri);

            //ViewData["JsSdkUiPackage"] = jssdkUiPackage;
            //ViewData["AppId"] = model.OfficialAccounts.AppID;
            //ViewData["Timestamp"] = jssdkUiPackage.Timestamp;
            //ViewData["NonceStr"] = jssdkUiPackage.NonceStr;
            //ViewData["Signature"] = jssdkUiPackage.Signature;
            //ViewData["Link"] = Request.Url.AbsoluteUri;
           

            return View(model);

        }
    }
}
