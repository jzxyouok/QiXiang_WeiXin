using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMP.BLL;
using QMP.BLL.SQL;
using QMP.Models;
using QMP.Models.SQL;

namespace QMP.Web.Areas.WeiXin.Controllers
{
    public class WarnInfosController : Controller
    {
        //
        // GET: /WeiXin/WarnInfos/

        public ActionResult List(Guid accountid)
        {

            WarnInfos_BLL bll = new WarnInfos_BLL();
            List<WarnInfos> list =
                bll.GetList(a => a.AccountID == accountid).OrderByDescending(a => a.ReleaseTime).ToList();

            foreach (var item in list)
            {
                item.Content = item.Content.Replace("\r\n", "<br/>");

            }
            return View(list);
        }

    }
}
