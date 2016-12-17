using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using QMP.ViewModels;

namespace QMP.BLL
{
    public class Cloud_BLL
    {
        private DAL.Cloud_DAL dal = new DAL.Cloud_DAL();
        public List<Cloud_ViewModel> GetLastCloud()
        {
            return dal.GetLastCloud();
        }

        public byte[] GetCloudImg(string name)
        {
            return dal.GetCloudImg(name);

        }
    }
}
