using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QMP.BLL.SQL;
using QMP.Models.Oracle;
using QMP.Models.SQL;

namespace QMP.Web.Areas.Super.Controllers
{
    public class StationController : Controller
    {
        // GET: Super/Station
        public ActionResult OracleToSql()
        {
            OracleSAEntities db = new OracleSAEntities();
            List<AWS_BASE_INFO> fList = db.AWS_BASE_INFO.Where(a => a.QXNAME == "平度市").ToList();


            AutoStations_BLL bll = new AutoStations_BLL();

            foreach (AWS_BASE_INFO awsBaseInfo in fList)
            {
                AutoStations sta = new AutoStations()
                {
                    AccountID=Guid.Parse("fdffff70-e7a1-4df0-a4b7-54815cb983ce"),
                    Latitude=double.Parse(awsBaseInfo.LAT.ToString()),
                    Longitude=double.Parse(awsBaseInfo.LON_.ToString()),
                    Country= "平度市",
                    StationName=awsBaseInfo.SNAME,
                    ShortName=awsBaseInfo.SNAME
                };
                bll.Add(sta,false);
            }
           var count= bll.SaveChange();
            return Content(count.ToString());
        }

        public ActionResult Service()
        {
            ServiceInfos_BLL bll = new ServiceInfos_BLL();
            ServiceInfos_Categorys_BLL cbll = new ServiceInfos_Categorys_BLL();
            List<ServiceInfos> yuanlist = bll.GetList().ToList();
            foreach (var service in yuanlist)
            {
                ServiceInfos_Categorys cate= cbll.Get(a =>a.AccountID == service.AccountID &&a.CategoryName == service.ServiceInfos_Categorys.CategoryName);
                service.CategoryID = cate.CategoryID;
                bll.Update(service,false);
            }
            
            return Content(bll.SaveChange().ToString());
        }



    }
}