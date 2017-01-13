using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QMP.DAL.SQL.Emergency;
using QMP.Models.Oracle;
using QMP.ViewModels.Emergency;

namespace QMP.BLL.SQL.Emergency
{
    public class Emergency_Station_BLL
    {
        private Emergency_Station_DAL dal = new Emergency_Station_DAL();
        public List<Emergency_Station_Data_ViewModel> GetLast(YTHPT_EMERGENCY_STATION station)
        {
            return dal.GetLast(station);
        }
    }
}
