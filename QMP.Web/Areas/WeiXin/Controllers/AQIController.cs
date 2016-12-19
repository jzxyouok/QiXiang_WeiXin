using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;



namespace QMP.Web.Areas.WeiXin.Controllers
{
    public class AQIController : Controller
    {
        //
        // GET: /WeiXin/AQI/

        public ActionResult Index(string city="胶州市")
        {
            ViewBag.City = city;
            return View();
        }

        public ActionResult Index2()
        {
            return Content("26465465");
            return View();
        }
        public ActionResult _GetAQIJson(string city = "胶州市")
        {
            return Content(HttpGet(city));
            //AirDataEntities db = new AirDataEntities();
            //List<stationInfo> slist = db.stationInfo.Where(a => a.staname.Contains("胶州")).ToList();
            ////List<stationInfo> slist = db.stationInfo.ToList();


            //List<AQI_Point_ViewModel> list = new List<AQI_Point_ViewModel>();
            //foreach (stationInfo station in slist)
            //{
            //    Data_Hour dh = db.Data_Hour.Where(a => a.sID == station.stationnum).OrderByDescending(a => a.date_time).FirstOrDefault();


            //    if (dh.SO2 == null)
            //    {
            //        dh.SO2 = 0;
            //    }
            //    if (dh.NO2 == null)
            //    {
            //        dh.NO2 = 0;
            //    }
            //    if (dh.O3 == null)
            //    {
            //        dh.O3 = 0;
            //    }
            //    if (dh.CO == null)
            //    {
            //        dh.CO = 0;
            //    }
            //    if (dh.PM2d5 == null)
            //    {
            //        dh.PM2d5 = 0;
            //    }
            //    if (dh.PM10 == null)
            //    {
            //        dh.PM10 = 0;
            //    }
            //    string str = " SELECT dbo.mathAQI(" + dh.SO2 + ", 'SO21') AS SO2, dbo.mathAQI(" + dh.NO2 + ", 'NO21') AS NO2, ";
            //    str += " dbo.mathAQI(" + dh.CO + ", 'CO1') AS CO, dbo.mathAQI(" + dh.O3 + ", 'O31') AS O3, ";
            //    str += " dbo.mathAQI(" + dh.PM2d5 + ", 'PM2.5') AS PM25, dbo.mathAQI(" + dh.PM10 + ", 'PM10') AS PM10";


            //    AQIMathResult mathResult = db.Database.SqlQuery<AQIMathResult>(str).ToList().FirstOrDefault();

            //    AQIHelper.GetAQI(mathResult);



            //    AQI_Point_ViewModel apv = new AQI_Point_ViewModel();



            //    apv.StaName = station.staname;
            //    apv.AQI = AQIHelper.GetAQI(mathResult);
            //    apv.StationNum = station.stationnum;
            //    apv.Longitude = (float)station.LON_;
            //    apv.Latitude = (float)station.LAT;
            //    list.Add(apv);

            //}

            //return Json(list, JsonRequestBehavior.AllowGet);
        }
        public string HttpGet(string city = "胶州市")  //get读取
        {

            string url = "http://172.18.226.106:88/jiaozhou/aqi/GetAQIJson";
            if (city == "莱西市")
            {
                url = "http://172.18.226.106:88/laixi/aqi/GetAQIJson";
            }
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = "GET";

            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream myResponseStream = response.GetResponseStream();

            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

            string retString = myStreamReader.ReadToEnd();

            myStreamReader.Close();

            myResponseStream.Close();

            return retString;

        }
    }
}
