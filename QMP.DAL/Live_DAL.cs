using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QMP.Models;
using QMP.Models.Oracle;
using QMP.ViewModels;
using QMP.Models.SQL;

namespace QMP.DAL
{
    public class Live_DAL
    {

        public List<LiveValue_ViewModel> GetLastRain()
        {
            OracleSAEntities db = new OracleSAEntities();
            string date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm");
            //string sql = "select staname as StaName,fdate as DateTime,hrain as Value,lon_ as Lon,lat as Lat,qxname as Country  from (select * from (select staname ,fdate,hrain from (select * from datatest  t1  right join (select staname as newstaname ,max(fdate) as newfdate from datatest where   fdate > to_date('" + date + "','yyyy-mm-dd hh24:mi')  and  hrain !='9999' group by staname) t2 on t1.fdate=t2.newfdate and t1.staname=t2.newstaname)) t3  inner join aws_base_info t4 on t3.staname = t4.sname)";


            string ydate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm");
            string tdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");



            //string sql = "select staname as StaName,fdate as DateTime,hrain as Value,lon_ as Lon,lat as Lat,qxname as Country  from (select * from (select staname ,fdate,hrain from (select * from datatest  t1  right join (select staname as newstaname ,max(fdate) as newfdate from datatest where   fdate > to_date('" + date + "','yyyy-mm-dd hh24:mi')  and  hrain !='9999' group by staname) t2 on t1.fdate=t2.newfdate and t1.staname=t2.newstaname)) t3  inner join aws_base_info t4 on t3.staname = t4.sname)";
            string sql = "select staname as StaName,sum(hrain) as Value  from DATAMINUTE  where fdate between to_date('" + ydate + "','yyyy-mm-dd hh24:mi') and  to_date('" + tdate + "','yyyy-mm-dd hh24:mi') group by staname";



            List<LiveValue_ViewModel> list = db.Database.SqlQuery<LiveValue_ViewModel>(sql).ToList();
            foreach (var item in list)
            {
                item.DateString = item.DateTime.ToString("MM月dd日HH时mm分");
            }

            QMPEntities qdb = new QMPEntities();
            List<AutoStations> stationlist = qdb.AutoStations.Where(a => a.Country == "胶州市").ToList();


            List<LiveValue_ViewModel> vlist = new List<LiveValue_ViewModel>();

            foreach (AutoStations station in stationlist)
            {
                LiveValue_ViewModel lv = list.Where(a => a.StaName == station.StationName).FirstOrDefault();
                if (lv != null)
                {
                    LiveValue_ViewModel vmodel = new LiveValue_ViewModel();
                    vmodel.StaName = station.ShortName;
                    vmodel.Lon = (float)station.Longitude;
                    vmodel.Lat = (float)station.Latitude;
                    vmodel.Value = lv.Value;
                    vmodel.DateTime = lv.DateTime;
                    vmodel.DateString = lv.DateString;
                    vmodel.Country = station.Country;
                    vlist.Add(vmodel);
                }
            }


            return vlist;
        }
        public List<LiveValue_ViewModel> GetLastRain(string country)
        {
            OracleSAEntities db = new OracleSAEntities();
            string date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm");



            string ydate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm");
            string tdate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");



            //string sql = "select staname as StaName,fdate as DateTime,hrain as Value,lon_ as Lon,lat as Lat,qxname as Country  from (select * from (select staname ,fdate,hrain from (select * from datatest  t1  right join (select staname as newstaname ,max(fdate) as newfdate from datatest where   fdate > to_date('" + date + "','yyyy-mm-dd hh24:mi')  and  hrain !='9999' group by staname) t2 on t1.fdate=t2.newfdate and t1.staname=t2.newstaname)) t3  inner join aws_base_info t4 on t3.staname = t4.sname)";
            string sql = "select staname as StaName,sum(hrain) as Value  from DATAMINUTE  where fdate between to_date('" + ydate + "','yyyy-mm-dd hh24:mi') and  to_date('" + tdate + "','yyyy-mm-dd hh24:mi') group by staname";



            List<RainValue_ViewModel> list = db.Database.SqlQuery<RainValue_ViewModel>(sql).ToList();

            //foreach (var item in list)
            //{
            //    item.DateString = item.DateTime.ToString("MM月dd日HH时mm分");
            //}

            QMPEntities qdb = new QMPEntities();
            List<AutoStations> stationlist = qdb.AutoStations.Where(a => a.Country == country).ToList();


            List<LiveValue_ViewModel> vlist = new List<LiveValue_ViewModel>();

            foreach (AutoStations station in stationlist)
            {
                RainValue_ViewModel lv = list.Where(a => a.StaName == station.StationName).FirstOrDefault();
                if (lv != null)
                {
                    LiveValue_ViewModel vmodel = new LiveValue_ViewModel();
                    vmodel.StaName = station.ShortName;
                    vmodel.Lon = (float)station.Longitude;
                    vmodel.Lat = (float)station.Latitude;
                    vmodel.Value = (float)lv.Value;
                    //vmodel.DateTime = lv.DateTime;
                    vmodel.DateString = ydate + "~" + tdate;
                    vmodel.Country = station.Country;
                    vlist.Add(vmodel);
                }
            }


            return vlist;
        }

        public List<LiveValue_ViewModel> GetLastTemp()
        {
            OracleSAEntities db = new OracleSAEntities();
            string date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm");
            string sql = "select staname as StaName,fdate as DateTime,TEMPERATURE as Value,lon_ as Lon,lat as Lat,qxname as Country  from (select * from (select staname ,fdate,TEMPERATURE from (select * from datatest  t1  right join (select staname as newstaname ,max(fdate) as newfdate from datatest where   fdate > to_date('" + date + "','yyyy-mm-dd hh24:mi')  and  TEMPERATURE !='9999' group by staname) t2 on t1.fdate=t2.newfdate and t1.staname=t2.newstaname)) t3  inner join aws_base_info t4 on t3.staname = t4.sname)";
            List<LiveValue_ViewModel> list = db.Database.SqlQuery<LiveValue_ViewModel>(sql).ToList();
            foreach (var item in list)
            {
                item.DateString = item.DateTime.ToString("MM月dd日HH时mm分");
            }

            QMPEntities qdb = new QMPEntities();
            List<AutoStations> stationlist = qdb.AutoStations.Where(a => a.Country == "胶州市").ToList();


            List<LiveValue_ViewModel> vlist = new List<LiveValue_ViewModel>();

            foreach (AutoStations station in stationlist)
            {
                LiveValue_ViewModel lv = list.Where(a => a.StaName == station.StationName).FirstOrDefault();
                if (lv != null)
                {
                    LiveValue_ViewModel vmodel = new LiveValue_ViewModel();
                    vmodel.StaName = station.ShortName;
                    vmodel.Lon = (float)station.Longitude;
                    vmodel.Lat = (float)station.Latitude;
                    vmodel.Value = lv.Value;
                    vmodel.DateTime = lv.DateTime;
                    vmodel.DateString = lv.DateString;
                    vmodel.Country = station.Country;
                    vlist.Add(vmodel);
                }
            }


            return vlist;
        }
        public List<LiveValue_ViewModel> GetLastTemp(string country)
        {
            OracleSAEntities db = new OracleSAEntities();
            string date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm");

            string sql = "select staname as StaName,fdate as DateTime,TEMPERATURE as Value,lon_ as Lon,lat as Lat,qxname as Country  from (select * from (select staname ,fdate,TEMPERATURE from (select * from datatest  t1  right join (select staname as newstaname ,max(fdate) as newfdate from datatest where   fdate > to_date('" + date + "','yyyy-mm-dd hh24:mi')  and  TEMPERATURE !='9999' group by staname) t2 on t1.fdate=t2.newfdate and t1.staname=t2.newstaname)) t3  inner join aws_base_info t4 on t3.staname = t4.sname)";
            List<LiveValue_ViewModel> list = db.Database.SqlQuery<LiveValue_ViewModel>(sql).ToList();
            list = list.Where(a => a.Country == country).ToList();

            foreach (var item in list)
            {
                item.DateString = item.DateTime.ToString("MM月dd日HH时mm分");
            }

            QMPEntities qdb = new QMPEntities();
            List<AutoStations> stationlist = qdb.AutoStations.Where(a => a.Country == country).ToList();


            List<LiveValue_ViewModel> vlist = new List<LiveValue_ViewModel>();

            foreach (AutoStations station in stationlist)
            {
                LiveValue_ViewModel lv = list.Where(a => a.StaName == station.StationName).FirstOrDefault();
                if (lv != null)
                {
                    LiveValue_ViewModel vmodel = new LiveValue_ViewModel();
                    vmodel.StaName = station.ShortName;
                    vmodel.Lon = (float)station.Longitude;
                    vmodel.Lat = (float)station.Latitude;
                    vmodel.Value = lv.Value;
                    vmodel.DateTime = lv.DateTime;
                    vmodel.DateString = lv.DateString;
                    vmodel.Country = station.Country;
                    vlist.Add(vmodel);
                }
            }


            return vlist;
        }
        public List<Wind_ViewModel> GetLastWind()
        {
            OracleSAEntities db = new OracleSAEntities();
            string date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm");
            string sql = "select staname as StaName,fdate as DateTime,TWSPEED as WindSpeed,TWDIRECTION as WindDirection ,lon_ as Lon,lat as Lat,qxname as Country from (select * from (select staname ,fdate,TWSPEED,TWDIRECTION from (select * from datatest  t1  right join (select staname as newstaname ,max(fdate) as newfdate from datatest where   fdate > to_date('" + date + "','yyyy-mm-dd hh24:mi') and  TWSPEED !='9999' and TWDIRECTION !='9999' group by staname) t2 on t1.fdate=t2.newfdate and t1.staname=t2.newstaname)) t3  inner join aws_base_info t4 on t3.staname = t4.sname)";
            List<Wind_ViewModel> list = db.Database.SqlQuery<Wind_ViewModel>(sql).ToList();
            foreach (var item in list)
            {
                item.DateString = item.DateTime.ToString("MM月dd日HH时mm分");
            }



            /////////////////////////////////////////////////
       
            QMPEntities qdb = new QMPEntities();
            List<AutoStations> stationlist = qdb.AutoStations.Where(a => a.Country == "胶州市").ToList();


            List<Wind_ViewModel> vlist = new List<Wind_ViewModel>();

            foreach (AutoStations station in stationlist)
            {
                Wind_ViewModel lv = list.Where(a => a.StaName == station.StationName).FirstOrDefault();
                if (lv != null)
                {
                    Wind_ViewModel vmodel = new Wind_ViewModel();
                    vmodel.StaName = station.ShortName;
                    vmodel.Lon = (float)station.Longitude;
                    vmodel.Lat = (float)station.Latitude;
                    vmodel.WindSpeed = lv.WindSpeed;
                    vmodel.WindDirection = lv.WindDirection;
                    vmodel.DateTime = lv.DateTime;
                    vmodel.DateString = lv.DateString;
                    vmodel.Country = station.Country;
                    vlist.Add(vmodel);
                }
            }


            return vlist;
        }
        public List<Wind_ViewModel> GetLastWind(string country)
        {
            OracleSAEntities db = new OracleSAEntities();
            string date = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm");

            string sql = "select staname as StaName,fdate as DateTime,TWSPEED as WindSpeed,TWDIRECTION as WindDirection ,lon_ as Lon,lat as Lat,qxname as Country from (select * from (select staname ,fdate,TWSPEED,TWDIRECTION from (select * from datatest  t1  right join (select staname as newstaname ,max(fdate) as newfdate from datatest where   fdate > to_date('" + date + "','yyyy-mm-dd hh24:mi') and  TWSPEED !='9999' and TWDIRECTION !='9999' group by staname) t2 on t1.fdate=t2.newfdate and t1.staname=t2.newstaname)) t3  inner join aws_base_info t4 on t3.staname = t4.sname)";
            List<Wind_ViewModel> list = db.Database.SqlQuery<Wind_ViewModel>(sql).ToList();

            list = list.Where(a => a.Country == country).ToList();

            foreach (var item in list)
            {
                item.DateString = item.DateTime.ToString("MM月dd日HH时mm分");
            }

            /////////////////////////////////////////////////

            QMPEntities qdb = new QMPEntities();
            List<AutoStations> stationlist = qdb.AutoStations.Where(a => a.Country == country).ToList();


            List<Wind_ViewModel> vlist = new List<Wind_ViewModel>();

            foreach (AutoStations station in stationlist)
            {
                Wind_ViewModel lv = list.Where(a => a.StaName == station.StationName).FirstOrDefault();
                if (lv != null)
                {
                    Wind_ViewModel vmodel = new Wind_ViewModel();
                    vmodel.StaName = station.ShortName;
                    vmodel.Lon = (float)station.Longitude;
                    vmodel.Lat = (float)station.Latitude;
                    vmodel.WindSpeed = lv.WindSpeed;
                    vmodel.WindDirection = lv.WindDirection;
                    vmodel.DateTime = lv.DateTime;
                    vmodel.DateString = lv.DateString;
                    vmodel.Country = station.Country;
                    vlist.Add(vmodel);
                }
            }


            return vlist;
        }

        public List<HistoryChartValue_ViewModel> GetLast24HourData(string staname)
        {
            OracleSAEntities db = new OracleSAEntities();

            DateTime predate = DateTime.Now.AddHours(-24);
            DateTime nextdate = DateTime.Now;
            List<DATAHOUR> list =
                db.DATAHOUR.Where(a => a.STANAME == staname && a.FDATE > predate && a.FDATE <= nextdate).ToList();

            List<HistoryChartValue_ViewModel> vlist = new List<HistoryChartValue_ViewModel>();
            foreach (var item in list)
            {
                HistoryChartValue_ViewModel view = new HistoryChartValue_ViewModel
                {
                    StaName = item.STANAME,
                    FDate = item.FDATE,
                    HourString = item.FDATE.ToString("HH"),
                    Rain = item.HRAIN,
                    Temperature = item.TEMPERATURE,
                    WindSpeed = item.TWSPEED,
                    DateString = item.FDATE.ToString("MM月dd日HH时mm分")
                };

                vlist.Add(view);
            }

            return vlist;
        }
    }
}
