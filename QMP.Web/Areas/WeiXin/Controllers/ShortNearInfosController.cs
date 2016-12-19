using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMP.BLL;
using QMP.Models;

namespace QMP.Web.Areas.WeiXin.Controllers
{
    public class ShortNearInfosController : Controller
    {
        //
        // GET: /WeiXin/ShortNearInfos/

        public ActionResult Details(Guid id)
        {


            ShortNearInfos_BLL bll = new ShortNearInfos_BLL();
            ShortNearInfos model = bll.Get(a => a.InfoID == id);

            return View(model);


        }

    }
}
