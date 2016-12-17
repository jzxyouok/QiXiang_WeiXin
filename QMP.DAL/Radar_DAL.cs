using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using QMP.Utility;
using QMP.ViewModels;

namespace QMP.DAL
{
    public class Radar_DAL
    {

        public List<Radar_ViewModel> GetLastRadar()
        {
            List<FileInfo> list = FileHelper.GetFileInfos(@"D:\WebRoot\Web\radar");

           list= list.Take(10).OrderBy(a=>a.Name).ToList();




            List<Radar_ViewModel> vlist = new List<Radar_ViewModel>();
            foreach (FileInfo fileInfo in list)
            {
                Radar_ViewModel vv = new Radar_ViewModel();
                vv.Name = fileInfo.Name;

                DateTime shijian =
                    DateTime.Parse(fileInfo.Name.Substring(6, 4) + "-" + fileInfo.Name.Substring(10, 2) + "-" +
                                   fileInfo.Name.Substring(12, 2) + " " + fileInfo.Name.Substring(14, 2) + ":" +
                                   fileInfo.Name.Substring(16, 2)).AddHours(8);

                vv.ShortName = shijian.ToString("HH:mm");
                //vv.Url=
                vlist.Add(vv);
            }
            return vlist;
        }

        public byte[] GetRadarImg(string name)
        {
          byte[]  content=  FileHelper.GetImg(@"D:\WebRoot\Web\radar\" + name);
            return content;
        }





        public List<Radar_ViewModel> Get178LastRadar()
        {
            List<FileInfo> list = FileHelper.GetFileInfos(@"\\172.18.226.178\cmacast\RADA_PUB\NOR\IMG", "*_R_ABHH*GIF");

            list = list.Take(20).OrderBy(a => a.Name).ToList();




            List<Radar_ViewModel> vlist = new List<Radar_ViewModel>();
            foreach (FileInfo fileInfo in list)
            {
                Radar_ViewModel vv = new Radar_ViewModel();
                vv.Name = fileInfo.Name;

                DateTime shijian =
                    DateTime.Parse(fileInfo.Name.Substring(14, 4) + "-" + fileInfo.Name.Substring(18, 2) + "-" +
                                   fileInfo.Name.Substring(20, 2) + " " + fileInfo.Name.Substring(22, 2) + ":" +
                                   fileInfo.Name.Substring(24, 2)).AddHours(8);

                vv.ShortName = shijian.ToString("HH:mm");
                //vv.Url=
                vlist.Add(vv);
            }
            return vlist;
        }

        public byte[] Get178RadarImg(string name)
        {
            byte[] content = FileHelper.GetImg(@"\\172.18.226.178\cmacast\RADA_PUB\NOR\IMG\" + name);
            return content;
        }

    }
}
