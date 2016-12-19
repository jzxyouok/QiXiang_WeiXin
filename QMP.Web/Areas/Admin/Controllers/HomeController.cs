using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMP.BLL;
using QMP.BLL.SQL;
using QMP.Models;
using QMP.Models.SQL;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.Analysis;
using Senparc.Weixin.MP.AdvancedAPIs.User;
using Senparc.Weixin.MP.CommonAPIs;
using Senparc.Weixin.MP.Containers;

namespace QMP.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //
        // GET: /Admin/Home/

        public ActionResult Index()
        {
            Users user = new Users_BLL().GetCurrentUser();

            string appid = user.OfficialAccounts.AppID;
            if (!AccessTokenContainer.CheckRegistered(appid)) //检查是否已经注册
            {
                AccessTokenContainer.Register(appid, user.OfficialAccounts.AppSecret); //如果没有注册则进行注册
            }
            //string sdate = DateTime.Now.AddDays(-5).ToShortDateString();
            //string edate = DateTime.Now.AddDays(-1).ToShortDateString();

            //AnalysisResultJson<UserCumulateItem> aaa = AnalysisApi.GetUserCumulate(appid, sdate, edate);

            //var aaaaaaa = aaa;

            OpenIdResultJson users = UserApi.Get(appid, null);
            ViewBag.SubsribesTotalCount = users.total;



            ViewBag.MessagesTotalCount = new Messages_BLL().GetCount(a => a.AccountID == user.AccountID);
            ViewBag.WarnCurrentCount = new WarnInfos_BLL().GetCount(a => a.AccountID == user.AccountID);
            ViewBag.InfoTotalCount = new ServiceInfos_BLL().GetCount(a => a.AccountID == user.AccountID);

          
          

            return View();
        }

        public PartialViewResult _LayoutTitle()
        {
            Users_BLL bll = new Users_BLL();
            Users user = bll.GetCurrentUser();
            return PartialView(user);
        }

    }
}
