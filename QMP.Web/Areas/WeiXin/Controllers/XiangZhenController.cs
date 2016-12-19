using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using QMP.ViewModels.XiangZhen;

namespace QMP.Web.Areas.WeiXin.Controllers
{
    public class XiangZhenController : Controller
    {
        //
        // GET: /WeiXin/XianZhen/

        public ActionResult Index()
        {
            string url = "http://172.18.226.109/QuXian/JiaoZhou/GetXiangZhenJson.ashx";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "text/html;charset=UTF-8";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();



            JavaScriptSerializer jssl = new JavaScriptSerializer();
            //var bbb = jssl.DeserializeObject(retString);

          XiangZhen_ViewModel vm =   jssl.Deserialize<XiangZhen_ViewModel>(retString);

            

           

            return View(vm);
        }


        public ActionResult Map()
        {
        
            ViewBag.City = "胶州市";


            return View();
        }

        public ActionResult _GetXiangZhenAjax(string city)
        {
            string url = "http://172.18.226.109/QuXian/JiaoZhou/GetXiangZhenJson.ashx";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "text/html;charset=UTF-8";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();



            JavaScriptSerializer jssl = new JavaScriptSerializer();
            //var bbb = jssl.DeserializeObject(retString);

            XiangZhen_ViewModel vm = jssl.Deserialize<XiangZhen_ViewModel>(retString);
            return Json(vm, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Index2()
        {
            string url = "http://172.18.226.109/QuXian/JiaoZhou/GetXiangZhenJson.ashx";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "text/html;charset=UTF-8";
            request.ContentType = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();



            JavaScriptSerializer jssl = new JavaScriptSerializer();
            //var bbb = jssl.DeserializeObject(retString);

            XiangZhen_ViewModel vm = jssl.Deserialize<XiangZhen_ViewModel>(retString);





            return View(vm);
        }

    }
}
