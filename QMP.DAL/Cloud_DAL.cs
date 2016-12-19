using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HaiRui.File;
using QMP.ViewModels;

namespace QMP.DAL
{
    public class Cloud_DAL
    {
        public List<Cloud_ViewModel> GetLastCloud()
        {
            //List<FileInfo> list = FileHelper.GetShareFileInfos(@"\\172.18.226.22\cloud_new", "FY2D_*", "administrator",
            List<FileInfo> list = FileHelper.GetFileInfos(@"D:\WebRoot\Web\cloud_new");

            list = list.Take(10).OrderBy(a => a.Name).ToList();




            List<Cloud_ViewModel> vlist = new List<Cloud_ViewModel>();
            foreach (FileInfo fileInfo in list)
            {
                Cloud_ViewModel vv = new Cloud_ViewModel();
                vv.Name = fileInfo.Name;

                DateTime shijian =
                    DateTime.Parse(fileInfo.Name.Substring(5, 4) + "-" + fileInfo.Name.Substring(10, 2) + "-" +
                                   fileInfo.Name.Substring(13, 2) + " " + fileInfo.Name.Substring(16, 2) + ":" +
                                   fileInfo.Name.Substring(19, 2)).AddHours(8);

                vv.ShortName = shijian.ToString("HH:mm");
                //vv.Url=
                vlist.Add(vv);
            }
            return vlist.OrderBy(a=>a.ShortName).ToList();
        }

        public byte[] GetCloudImg(string name)
        {
            byte[] content = FileHelper.GetImg(@"D:\WebRoot\Web\cloud_new\" + name);
            return content;
        }
    }
}
