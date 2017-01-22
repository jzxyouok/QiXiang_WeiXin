using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMP.Models.Oracle;
using QMP.ViewModels;
using QMP.ViewModels.Emergency;

namespace QMP.Web.Areas.WeiXin.Controllers
{
    public class EmergencyController : Controller
    {
        // GET: WeiXin/Emergency
        public ActionResult Map()
        {
            OracleSAEntities db = new OracleSAEntities();


            List<YTHPT_EMERGENCY_SERVICE> slist = db.YTHPT_EMERGENCY_SERVICE.Where(a => a.COUNTRY.Contains("城阳") && a.STATE != "结束").OrderByDescending(a => a.CREATETIME).ToList();
            List<YTHPT_EMERGENCY_WEATHER> wlist = new List<YTHPT_EMERGENCY_WEATHER>();

            foreach (var item in slist)
            {
                List<YTHPT_EMERGENCY_WEATHER> ilist = db.YTHPT_EMERGENCY_WEATHER.Where(a => a.EMERGENCY==item.NAME).OrderByDescending(a => a.RELEASETIME).ToList();
                wlist.AddRange(ilist);
            }


           

            if (slist.Count() > 0)
            {
                ViewBag.Title = "城阳区" + slist.FirstOrDefault().NAME;

            }
            else
            {
                ViewBag.Title = "城阳区应急服务";

            }
            return View(wlist);
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

        public JsonResult _GetEmergencyStationLastJson()
        {
            OracleSAEntities db = new OracleSAEntities();

            List<YTHPT_EMERGENCY_SERVICE> serlist = db.YTHPT_EMERGENCY_SERVICE.Where(a => a.COUNTRY.Contains("城阳") && a.STATE != "结束").OrderByDescending(a => a.CREATETIME).ToList();

            List<YTHPT_EMERGENCY_STATION> slist = new List<YTHPT_EMERGENCY_STATION>();
            foreach (var item in serlist)
            {
                List<YTHPT_EMERGENCY_STATION> ilist = db.YTHPT_EMERGENCY_STATION.Where(a => a.EMERGENCY == item.NAME).OrderByDescending(a => a.CREATETIME).ToList();
                slist.AddRange(ilist);
            }

           

            List<Emergency_Station_Data_ViewModel> totallist = new List<Emergency_Station_Data_ViewModel>();
            foreach (var station in slist)
            {
                QMP.BLL.SQL.Emergency.Emergency_Station_BLL bll = new BLL.SQL.Emergency.Emergency_Station_BLL();
             List< Emergency_Station_Data_ViewModel> vlist=   bll.GetLast(station);
                totallist.AddRange(vlist);

            }
            return Json(totallist, JsonRequestBehavior.AllowGet);
        }

        public JsonResult _GetEmergencyServiceLastJson()
        {
            OracleSAEntities db = new OracleSAEntities();
            List<YTHPT_EMERGENCY_SERVICE> list = db.YTHPT_EMERGENCY_SERVICE.Where(a => a.COUNTRY.Contains("城阳")&&a.STATE!="结束").OrderByDescending(a => a.CREATETIME).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);

        }
    }
}