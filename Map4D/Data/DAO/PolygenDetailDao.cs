using CommonLogger.Libraries;
using Map4D.Models;
using Map4D.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Map4D.Data.DAO
{
    public class PolygenDetailDao
    {
        private AdoHelper helper = null;
        /// <summary>
        /// Construct Open SqlConnection
        /// </summary>
        public PolygenDetailDao()
        {
            string connection = AdoHelper.ConnectionString;
            helper = new AdoHelper(connection);
        }
       public List<PolygonDetailViewModel> GetDetailByLatLng(string lat,string lng)
        {
            List<PolygonDetailViewModel> polygonDetails = new List<PolygonDetailViewModel>();
            List<Position> positions = new List<Position>();
            string query = $"EXEC XacDinhToaDo @lat='{lat}' ,@lng='{lng}'";
            SqlDataReader reader = helper.ExecDataReader(query);
            while (reader.Read())
            {
                PolygonDetailViewModel Ward = new PolygonDetailViewModel
                {
                    Id =reader["Id"].ToString(),
                    DuLieuDoiTuong = reader["DuLieuDoiTuong"].ToString(),
                    //lay toa do
                    
                    Value = reader["Value"].ToString()
                };
                polygonDetails.Add(Ward);
            }
            reader.Close();
            return polygonDetails;
        }

        public static bool KiemTra2(Position pointKiemTra, List<Position> positions)
        {
            int i, j;
            bool c = false;
            int count = positions.Count;
            for (i = 0, j = count - 1; i < count; j = i++)
            {
                if (((positions[i].Lat > pointKiemTra.Lat) != (positions[j].Lat > pointKiemTra.Lat)) &&
                 (pointKiemTra.Lng < (positions[j].Lng - positions[i].Lng) * (pointKiemTra.Lat - positions[i].Lat)
                 / (positions[j].Lat - positions[i].Lat) + positions[i].Lng))
                {
                    c = !c;
                    break;
                }
                Console.WriteLine(c + "----OUT----" + i);
            }
            return c;
        }
    }
}