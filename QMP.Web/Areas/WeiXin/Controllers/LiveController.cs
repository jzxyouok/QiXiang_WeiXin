using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using QMP.BLL;
using QMP.BLL.SQL;
using QMP.Models;
using QMP.Models.SQL;
using QMP.ViewModels;
using Point = DotNet.Highcharts.Options.Point;

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
        public ActionResult Rain(string city = "胶州市")
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


        public ActionResult History(string staname = "胶州")
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

        public ActionResult HistoryDotNet(string staname)
        {

            Live_BLL bll = new Live_BLL();

        
            AutoStations_BLL abll = new AutoStations_BLL();
            AutoStations station = abll.Get(a => a.ShortName == staname);

        

            List<HistoryChartValue_ViewModel> list = bll.GetLast24HourData(station.StationName);




            Highcharts chart = new Highcharts("chart")
                .SetTitle(new Title { Text = staname+"最近24小时实况图" })
               
                .SetXAxis(new XAxis
                {
                    Categories = list.Select(a => a.HourString).ToArray(),
                    Type = AxisTypes.Datetime
                })
                .SetCredits(new Credits { Href = "", Text = "胶州天气" })

                  .SetYAxis(new YAxis[]
                {
                   new YAxis
                  {
                       Id="1",
                      Title = new YAxisTitle { Text = "降水",Style="color:'#2f97da'" },

                      Opposite=true,
                      Labels=new YAxisLabels {Formatter="function () {return this.value + ' mm';}",Style="color:'#2f97da'" }
                   }, new YAxis
                  {
                       Id="2",
                      Title = new YAxisTitle { Text = "风速",Style="color:'#f15bda'" },
                      Opposite=true,

                      Labels=new YAxisLabels {Formatter="function () {return this.value + ' m/s';}",Style="color:'#f15bda'" }

                   }, new YAxis
                  {
                       Id="3",
                      Title = new YAxisTitle { Text = "温度",Style="color:'#30b197'"  },
                      Opposite=false,
                      
                      Labels=new YAxisLabels {Formatter="function () {return this.value + ' ℃';}",Style="color:'#30b197'" }
                      
                   }
                })
                .SetSeries(new[]
                {
                    new Series
                    {

                        Type = ChartTypes.Column,
                        Name = "降水",
                       YAxis="1",
                       Color=Color.FromArgb(47,151,218),
                        Data = new Data(
                            list.Select(a=>new Point {Y= (double?)a.Rain}).ToArray()
                            ),
                        
                        ZIndex=1,
                        PlotOptionsColumn=new PlotOptionsColumn
                    {
                        DataLabels = new PlotOptionsColumnDataLabels
                        {
                            Color = Color.White,
                            Enabled = true
                        },
                        EnableMouseTracking = true
                    }

                    },
                    new Series
                    {

                        Type = ChartTypes.Spline,
                        Name = "风速",
                       YAxis="2",
                       Color=Color.FromArgb(241,91,0),
                        Data = new Data(
                           list.Select(a=>new Point {Y= (double?)a.WindSpeed}).ToArray()
                            ),
                        ZIndex=2,
                         PlotOptionsLine=new PlotOptionsLine
                    {
                        DataLabels = new PlotOptionsLineDataLabels
                        {
                            Color = Color.White,
                            Enabled = true
                        },
                        EnableMouseTracking = true
                    }

                    },
                    new Series
                    {

                        Type = ChartTypes.Spline,
                        Name = "温度",
                       Color=Color.FromArgb(48,177,151),
                       YAxis="3",
                        Data = new Data(
                            list.Select(a=>new Point {Y= (double?)a.Temperature}).ToArray()
                            ),
                        ZIndex=3,
                         PlotOptionsLine=new PlotOptionsLine
                    {
                        DataLabels = new PlotOptionsLineDataLabels
                        {
                            Color = Color.White,
                            Enabled = true
                        },
                        EnableMouseTracking = true
                    }

                    }
                })
                .SetTooltip(new Tooltip
                {
                    Crosshairs = new Crosshairs(true),
                    Shared = true
                })
                ;

            return View(chart);
        }


    }
}
