using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMP.Models.Oracle;
using QMP.ViewModels;

namespace QMP.Web.Areas.WeiXin.Controllers
{
    public class EmergencyController : Controller
    {
        // GET: WeiXin/Emergency
        public ActionResult Map()
        {
            OracleSAEntities db = new OracleSAEntities();
            List<YTHPT_EMERGENCY_WEATHER> list = db.YTHPT_EMERGENCY_WEATHER.Where(a => a.COUNTRY == "城阳").OrderByDescending(a=>a.RELEASETIME).ToList();

            return View(list);
        }

        public JsonResult _GetSkListJson()
        {
            OracleSAEntities db = new OracleSAEntities();


            string lastdate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm");

            string sql = "select staname ,temperature,dwspeed,dwdirection,visibility,hrain,fdate,lat,lon_ as lon,qxname from (select * from (select * from (select staname,temperature,dwspeed,dwdirection,visibility,hrain,fdate,row_number() over (partition by staname order by fdate desc) RN from datatest  where fdate > to_date('" + lastdate + "','yyyy-mm-dd hh24:mi')) where RN =1) t1 inner join aws_base_info t2 on t1.staname=t2.sname) t3 where t3.qxname='城阳区'";


            List<LiveValue_All_ViewModel> data = db.Database.SqlQuery<LiveValue_All_ViewModel>(sql).ToList();


            LiveValue_All_ViewModel_Result re = new LiveValue_All_ViewModel_Result();
            re.DateTime = ((DateTime)data.FirstOrDefault().FDATE).ToString("yyyy-MM-dd HH:mm");
            re.DataList = data;
            return Json(re, JsonRequestBehavior.AllowGet);
        }
    }
}