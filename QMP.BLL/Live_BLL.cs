using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QMP.DAL;
using QMP.Models.OracleSA;
using QMP.ViewModels;

namespace QMP.BLL
{
    public class Live_BLL
    {


        private Live_DAL dal = new Live_DAL();
        public List<LiveValue_ViewModel> GetLastRain()
        {
            return dal.GetLastRain();
        }

        public List<LiveValue_ViewModel> GetLastRain(string country)
        {
            return dal.GetLastRain(country);

        }

        public List<LiveValue_ViewModel> GetLastTemp()
        {
            return dal.GetLastTemp();
        }

        public List<LiveValue_ViewModel> GetLastTemp(string country)
        {
            return dal.GetLastTemp(country);

        }

        public List<Wind_ViewModel> GetLastWind()
        {
            return dal.GetLastWind();
        }

        public List<Wind_ViewModel> GetLastWind(string country)
        {
            return dal.GetLastWind(country);

        }

        public List<HistoryChartValue_ViewModel> GetLast24HourData(string staname)
        {




            return dal.GetLast24HourData(staname);
        }
    }
}
