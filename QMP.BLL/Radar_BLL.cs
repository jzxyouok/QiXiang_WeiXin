using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using QMP.ViewModels;

namespace QMP.BLL
{
    public class Radar_BLL
    {
        private DAL.Radar_DAL dal = new DAL.Radar_DAL();
        public List<Radar_ViewModel> GetLastRadar()
        {
            return dal.GetLastRadar();
        }

        public byte[] GetRadarImg(string name)
        {
            return dal.GetRadarImg(name);

        }
        public List<Radar_ViewModel> Get178LastRadar()
        {
            return dal.Get178LastRadar();
        }

        public byte[] Get178RadarImg(string name)
        {
            return dal.Get178RadarImg(name);

        }
    }
}
