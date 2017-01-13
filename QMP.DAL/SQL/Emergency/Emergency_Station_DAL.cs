using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using QMP.Models.Oracle;
using QMP.ViewModels.Emergency;

namespace QMP.DAL.SQL.Emergency
{
    public class Emergency_Station_DAL
    {
        public List<Emergency_Station_Data_ViewModel> GetLast(YTHPT_EMERGENCY_STATION station)
        {
            string connstr = ConfigurationManager.ConnectionStrings["CwqxData_70"].ToString();
            SqlConnection conn = new SqlConnection(connstr);
            conn.Open();
            string sqlstr = "select top 1 * from minute_zdz where staid= '"+station.STATIONNUMBER+"' order by fdate desc ";
            SqlCommand cmd = new SqlCommand(sqlstr, conn);
            SqlDataAdapter ad = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            ad.Fill(dt);
            conn.Close();
            List<Emergency_Station_Data_ViewModel> vlist = new List<Emergency_Station_Data_ViewModel>();
            foreach (DataRow dataRow in dt.Rows)
            {
                Emergency_Station_Data_ViewModel vmodel = new Emergency_Station_Data_ViewModel()
                {
                    StationName = station.STATIONNAME,
                    StationNumber = station.STATIONNUMBER,
                    Longitude = (decimal) station.LONGITUDE,
                    Latitude = (decimal) station.LATITUDE,
                    Country = station.COUNTRY,
                    EmergencyService = station.EMERGENCY,
                    DateTime = DateTime.Parse(dataRow["FDate"].ToString()),
                    WindDirection = decimal.Parse(dataRow["DWDirection"].ToString()),
                    WindSpeed = decimal.Parse(dataRow["DWSpeed"].ToString()),
                    Temperature = decimal.Parse(dataRow["Temperature"].ToString()),
                    Rain = decimal.Parse(dataRow["HRain"].ToString())
                };

                vlist.Add(vmodel);
            }
            return vlist;
        }
    }
}
