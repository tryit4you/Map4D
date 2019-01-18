using CommonLogger.Libraries;
using Map4D.Models;
using Map4D.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Map4D.Data.DAO
{
    public class PolygonDetailDao
    {
        private AdoHelper helper = null;
        /// <summary>
        /// Construct Open SqlConnection
        /// </summary>
        public PolygonDetailDao()
        {
            string connection = AdoHelper.ConnectionString;
            helper = new AdoHelper(connection);
        }
        public List<PolygonDetailViewModel> GetDetailByLatLng(string lat, string lng)
        {
            List<PolygonDetailViewModel> polygonDetails = new List<PolygonDetailViewModel>();
            string query = $"EXEC XacDinhToaDo @lat='{lat}' ,@lng='{lng}'";
            SqlDataReader reader = helper.ExecDataReader(query);
            while (reader.Read())
            {
                PolygonDetailViewModel Ward = new PolygonDetailViewModel
                {
                    Id = reader["Id"].ToString(),
                    DuLieuDoiTuong = reader["DuLieuDoiTuong"].ToString(),
                    Value = reader["Value"].ToString()
                };
                polygonDetails.Add(Ward);
            }
            reader.Close();
            return polygonDetails;
        }
        public bool KiemTraTonTai(PolygonDetailViewModel polygon,PointViewModel pointKiemTra)
        {
            string detail = ConvertStringJson(polygon.DuLieuDoiTuong);
            List<PointViewModel> listPoint = ConvertStringToListPoint(detail);
            bool result = KiemTraWithListPoint(listPoint, pointKiemTra);
            return result;
        }
        private string ConvertStringJson(string detail)
        {
            return detail.Substring(detail.IndexOf("[[") + 3, detail.LastIndexOf("]]") - detail.IndexOf("[[") - 4);
        }
        private List<PointViewModel> ConvertStringToListPoint(string detail)
        {
            List<PointViewModel> listPoint = new List<PointViewModel>();
            string[] listSplit = detail.Split('[', ']');
            foreach (string split in listSplit)
            {
                if (split.Length > 1)
                {
                    double Lng = double.Parse(split.Split(',')[0]);
                    double Lat = double.Parse(split.Split(',')[1]);
                    listPoint.Add(new PointViewModel() { Lng = Lng, Lat = Lat });
                }
            }
            return listPoint;
        }
        private bool KiemTraWithListPoint(List<PointViewModel> listPoint, PointViewModel pointKiemTra)
        {
            int i, j;
            bool result = false;
            int countListPoint = listPoint.Count;
            for (i = 0, j = countListPoint - 1; i < countListPoint; j = i++)
            {
                if (((listPoint[i].Lat > pointKiemTra.Lat) != (listPoint[j].Lat > pointKiemTra.Lat)) &&
                 (pointKiemTra.Lng < (listPoint[j].Lng - listPoint[i].Lng) * (pointKiemTra.Lat - listPoint[i].Lat)
                 / (listPoint[j].Lat - listPoint[i].Lat) + listPoint[i].Lng))
                {
                    result = !result;
                
                }
            }
            return result;
        }
        public bool IsInPolygon(List<PointViewModel> listPoint, PointViewModel pointKiemTra)
        {
            var coef = listPoint.Skip(1).Select((p, i) =>
                                            (pointKiemTra.Lat - listPoint[i].Lat) * (p.Lng - listPoint[i].Lng)
                                          - (pointKiemTra.Lng - listPoint[i].Lng) * (p.Lat - listPoint[i].Lat))
                                    .ToList();

            if (coef.Any(p => p == 0))
                return true;

            for (int i = 1; i < coef.Count(); i++)
            {
                if (coef[i] * coef[i - 1] < 0)
                    return false;
            }
            return true;
        }

        public bool KiemTraTonTai2(PolygonDetailViewModel polygon, PointViewModel pointKiemTra)
        {
            string detail = ConvertStringJson(polygon.DuLieuDoiTuong);
            List<PointViewModel> listPoint = ConvertStringToListPoint(detail);
            bool result = IsInPolygon(listPoint, pointKiemTra);
            return result;
        }

    }
}