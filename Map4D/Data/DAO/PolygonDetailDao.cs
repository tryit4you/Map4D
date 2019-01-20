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
        public InfoPointViewModel GetInfoPointByLatLng(string Lat, string Lng)
        {
            List<PolygonDetailViewModel> listDetail = GetDetailByLatLng(Lat, Lng);
            PointViewModel pointCheck = new PointViewModel() { Lng = double.Parse(Lng), Lat = double.Parse(Lat) };
            foreach (PolygonDetailViewModel polygon in listDetail)
            {
                bool result = CheckExitst(polygon, pointCheck);
                if (result)
                {
                    return GetInfoPointByWardCode(polygon.Value);
                }
            }
            return null;
        }
        public InfoPointViewModel GetInfoPointByWardCode(string wardCode)
        {
            string Ward = GetWardByCode(wardCode);
            string District = GetDistrictByWardCode(wardCode);
            string City = GetCityByWardCode(wardCode);
            return new InfoPointViewModel() { City = City, District = District, Ward = Ward };
        }
        private List<PolygonDetailViewModel> GetDetailByLatLng(string lat, string lng)
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
        private string GetCityByWardCode(string wardCode)
        {
            string City = string.Empty;
            string CityCode = wardCode.Substring(0, wardCode.Length - 6);
            string sqlQuery = "SELECT Name FROM Countries WHERE Code = '" + CityCode + "'";
            SqlDataReader reader = helper.ExecDataReader(sqlQuery);
            if (reader.Read())
            {
                City = reader["Name"].ToString();
            }
            reader.Close();
            return City;
        }
        private string GetDistrictByWardCode(string wardCode)
        {
            string District = string.Empty;
            string DistrictCode = wardCode.Substring(0, wardCode.Length - 3);
            string sqlQuery = "SELECT Name FROM Countries WHERE Code = '" + DistrictCode + "'";
            SqlDataReader reader = helper.ExecDataReader(sqlQuery);
            if (reader.Read())
            {
                District = reader["Name"].ToString();
            }
            reader.Close();
            return District;
        }
        private string GetWardByCode(string Code)
        {
            string Ward = string.Empty;
            string sqlQuery = "SELECT Name FROM Countries WHERE Code = '" + Code + "'";
            SqlDataReader reader = helper.ExecDataReader(sqlQuery);
            if (reader.Read())
            {
                Ward = reader["Name"].ToString();
            }
            reader.Close();
            return Ward;
        }
        private bool CheckExitst(PolygonDetailViewModel polygon,PointViewModel pointCheck)
        {
            List<PointViewModel> listPoint = ConvertDetailToListPoint(polygon.DuLieuDoiTuong);
            bool result = CheckWithListPoint(listPoint, pointCheck);
            return result;
        }
        private List<PointViewModel> ConvertDetailToListPoint(string detail)
        {
            List<PointViewModel> listPoint = new List<PointViewModel>();
            detail = detail.Substring(detail.IndexOf("[[") + 3, detail.LastIndexOf("]]") - detail.IndexOf("[[") - 4);
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
        private bool CheckWithListPoint(List<PointViewModel> listPoint, PointViewModel pointCheck)
        {
            int i, j;
            bool result = false;
            int countListPoint = listPoint.Count;
            for (i = 0, j = countListPoint - 1; i < countListPoint; j = i++)
            {
                if (((listPoint[i].Lat > pointCheck.Lat) != (listPoint[j].Lat > pointCheck.Lat)) &&
                 (pointCheck.Lng < (listPoint[j].Lng - listPoint[i].Lng) * (pointCheck.Lat - listPoint[i].Lat)
                 / (listPoint[j].Lat - listPoint[i].Lat) + listPoint[i].Lng))
                {
                    result = !result;
                }
            }
            return result;
        }
        public string GetPopupHtmlByCode(string Code)
        {
            string html = "<thead><tr><td colspan='2' style='font-weight:bold;'><span class='glyphicon glyphicon-tag'></span> Thông tin sơ lược</td></tr></thead><tbody><tr><td>Quốc gia</td><td>Việt Nam</td></tr>";
            if (Code.Length == 12)
            {
                InfoPointViewModel infoPoint = GetInfoPointByWardCode(Code);
                html += $"<tr><td>Tỉnh/Thành phố</td><td>{infoPoint.City}</td></tr><tr><td>Quận/Huyện</td><td>{infoPoint.District}</td></tr><tr><td>Xã/Phường</td><td>{infoPoint.Ward}</td></tr>";
                return html;
            }
            if (Code.Length == 9)
            {
                string District = GetDistrictByWardCode(Code + "000");
                string City = GetCityByWardCode(Code + "000");
                html += $"<tr><td>Tỉnh/Thành phố</td><td>{City}</td></tr><tr><td>Quận/Huyện</td><td>{District}</td></tr>";
                return html;
            }
            if (Code.Length == 6)
            {
                string City = GetCityByWardCode(Code + "000000");
                html += $"<tr><td>Tỉnh/Thành phố</td><td>{City}</td></tr>";
                return html;
            }
            return html + "</tbody>";
        }
    }
}