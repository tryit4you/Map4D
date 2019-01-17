using CommonLogger.Libraries;
using Map4D.Models;
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
       public List<PolygonDetailViewModel> GetDetailById(string lat,string lng)
        {
            List<PolygonDetailViewModel> polygonDetails = new List<PolygonDetailViewModel>();
            string query = $"EXEC XacDinhToaDo @lat='{lat}' ,@lng='{lng}'";
            SqlDataReader reader = helper.ExecDataReader(query);
            while (reader.Read())
            {
                PolygonDetailViewModel Ward = new PolygonDetailViewModel
                {
                    Id =reader["Id"].ToString(),
                    DuLieuDoiTuong = reader["DuLieuDoiTuong"].ToString(),
                    Value = reader["Value"].ToString()
                };
                polygonDetails.Add(Ward);
            }
            reader.Close();
            return polygonDetails;
        }
        
    }
}