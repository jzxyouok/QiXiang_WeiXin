using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMP.BLL;
using QMP.Models;
using QMP.ViewModels;

namespace QMP.Web.Areas.WeiXin.Controllers
{
    public class LiveController : Controller
    {
        //
        // GET: /WeiXin/Live/

        public ActionResult Index()
        {
            return View();
        }

        #region 降水
        public ActionResult Rain(string city="胶州市")
        {
            ViewBag.City = city;
            return View();
        }

        public JsonResult _GetRainAjax(string city = "胶州市")
        {
            Live_BLL bll = new Live_BLL();
            return Json(bll.GetLastRain(city), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 温度
        public ActionResult Temp(string city = "胶州市")
        {
            ViewBag.City = city;

            return View();
        }

        public JsonResult _GetTempAjax(string city = "胶州市")
        {
            ViewBag.City = city;
            Live_BLL bll = new Live_BLL();
            return Json(bll.GetLastTemp(city), JsonRequestBehavior.AllowGet);
        }
        #endregion 

        
        #region 风速
        public ActionResult Wind(string city = "胶州市")
        {
            ViewBag.City = city;



            return View();
        }

        public JsonResult _GetWindAjax(string city = "胶州市")
        {
            ViewBag.City = city;
            Live_BLL bll = new Live_BLL();
            return Json(bll.GetLastWind(city), JsonRequestBehavior.AllowGet);
        }
        #endregion


        public ActionResult History(string staname="胶州")
        {
            QMPEntities qdb = new QMPEntities();
            AutoStations station = qdb.AutoStations.Where(a => a.ShortName == staname).FirstOrDefault();



            ViewBag.StaName = station.StationName;
            ViewBag.ShortName = station.ShortName;
            return View();
        }

        public ActionResult GetLast24HourData(string staname)
        {
            Live_BLL bll = new Live_BLL();
            return Json(bll.GetLast24HourData(staname), JsonRequestBehavior.AllowGet);
        }


        public ActionResult Radar(string city = "胶州市")
        {
            ViewBag.City = city;
            Radar_BLL bll = new Radar_BLL();
            List<Radar_ViewModel> list = bll.GetLastRadar();
            return View(list);
        }

        public ActionResult GetRadarImg(string name)
        {
            Radar_BLL bll = new Radar_BLL();
            byte[] img = bll.GetRadarImg(name);
          return File(img, "image/jpeg");

        }
        public ActionResult Cloud(string city = "胶州市")
        {
            ViewBag.City = city;

            Cloud_BLL bll = new Cloud_BLL();
            List<Cloud_ViewModel> list = bll.GetLastCloud();
            return View(list);
        }

        public ActionResult GetCloudImg(string name)
        {
            Cloud_BLL bll = new Cloud_BLL();
            byte[] img = bll.GetCloudImg(name);
            return File(img, "image/jpeg");

        }


        public ActionResult MinuteForcase()
        {
            return View();
        }
    }
}
