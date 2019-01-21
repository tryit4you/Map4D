using CommonLogger.Libraries;
using Map4D.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;

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
        /// <summary>
        /// Get InfoPoint by LatLng
        /// </summary>
        /// <param name="Lat"></param>
        /// <param name="Lng"></param>
        /// <returns></returns>
        public InfoPointViewModel GetInfoPointByLatLng(string Lat, string Lng)
        {
            List<PolygonDetailViewModel> listDetail = GetDetailByLatLng(Lat, Lng);
            PointViewModel pointCheck = new PointViewModel() { Lng = double.Parse(Lng), Lat = double.Parse(Lat) };
            foreach (PolygonDetailViewModel polygon in listDetail)
            {
                bool result = CheckExitstPointInPolygon(polygon, pointCheck);
                if (result)
                {
                    return GetInfoPointByWardCode(polygon.Value);
                }
            }
            return null;
        }
        /// <summary>
        /// Get InfoPoint by WardCode
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public InfoPointViewModel GetInfoPointByWardCode(string wardCode)
        {
            string Ward = GetWardByWardCode(wardCode);
            string District = GetDistrictByWardCode(wardCode);
            string City = GetCityByWardCode(wardCode);
            return new InfoPointViewModel() { City = City, District = District, Ward = Ward };
        }
        /// <summary>
        /// Get ListPolygonDetail For LatLng
        /// </summary>
        /// <param name="Lat"></param>
        /// <param name="Lng"></param>
        /// <returns></returns>
        private List<PolygonDetailViewModel> GetDetailByLatLng(string Lat, string Lng)
        {
            List<PolygonDetailViewModel> listPolygonDetails = new List<PolygonDetailViewModel>();
            string sqlQuery = "EXEC XacDinhToaDo @lat = @_Lat ,@lng = @_Lng";
            object[] _params = new object[] { new SqlParameter("_Lat", Lat), new SqlParameter("_Lng", Lng) };

            SqlDataReader reader = helper.ExecDataReader(sqlQuery,_params);
            while (reader.Read())
            {
                PolygonDetailViewModel Ward = new PolygonDetailViewModel
                {
                    Id = reader["Id"].ToString(),
                    ObjectData = reader["DuLieuDoiTuong"].ToString(),
                    Value = reader["Value"].ToString()
                };
                listPolygonDetails.Add(Ward);
            }
            reader.Close();

            return listPolygonDetails;
        }
        /// <summary>
        /// Get City by WardCode
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        private string GetCityByWardCode(string wardCode)
        {
            string City = string.Empty;
            string CityCode = wardCode.Substring(0, wardCode.Length - 6);
            string sqlQuery = "SELECT Name FROM Countries WHERE Code = @CityCode";
            object[] _params = new object[] { new SqlParameter("CityCode", CityCode) };

            SqlDataReader reader = helper.ExecDataReader(sqlQuery,_params);
            if (reader.Read())
            {
                City = reader["Name"].ToString();
            }
            reader.Close();

            return City;
        }
        /// <summary>
        /// Get District by WardCode
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        private string GetDistrictByWardCode(string wardCode)
        {
            string District = string.Empty;
            string DistrictCode = wardCode.Substring(0, wardCode.Length - 3);
            string sqlQuery = "SELECT Name FROM Countries WHERE Code = @DistrictCode";
            object[] _params = new object[] { new SqlParameter("DistrictCode", DistrictCode) };

            SqlDataReader reader = helper.ExecDataReader(sqlQuery,_params);
            if (reader.Read())
            {
                District = reader["Name"].ToString();
            }
            reader.Close();

            return District;
        }
        /// <summary>
        /// Get Ward by wardCode
        /// </summary>
        /// <param name="wardCode"></param>
        /// <returns></returns>
        private string GetWardByWardCode(string wardCode)
        {
            string Ward = string.Empty;
            string sqlQuery = "SELECT Name FROM Countries WHERE Code = @wardCode";
            object[] _params = new object[] { new SqlParameter("wardCode", wardCode) };

            SqlDataReader reader = helper.ExecDataReader(sqlQuery,_params);
            if (reader.Read())
            {
                ward = reader["Name"].ToString();
            }
            reader.Close();

            return Ward;
        }
        /// <summary>
        /// Check Exitst Point In Polygon
        /// </summary>
        /// <param name="polygon"></param>
        /// <param name="pointCheck"></param>
        /// <returns></returns>
        private bool CheckExitstPointInPolygon(PolygonDetailViewModel polygon,PointViewModel pointCheck)
        {
            List<PointViewModel> listPointTop = ConvertObjectDataToListPoint(polygon.ObjectData);
            return CheckWithListPoint(listPointTop, pointCheck);
        }
        /// <summary>
        /// Convert ObjectData to ListPoint
        /// </summary>
        /// <param name="ObjectData"></param>
        /// <returns></returns>
        private List<PointViewModel> ConvertObjectDataToListPoint(string ObjectData)
        {
            List<PointViewModel> listPoint = new List<PointViewModel>();
            ObjectData = ObjectData.Substring(ObjectData.IndexOf("[[") + 3, ObjectData.LastIndexOf("]]") - ObjectData.IndexOf("[[") - 4);
            string[] listSplit = ObjectData.Split('[', ']');

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
        /// <summary>
        /// Check Exitst with ListPoint
        /// </summary>
        /// <param name="listPoint"></param>
        /// <param name="pointCheck"></param>
        /// <returns></returns>
        private bool CheckWithListPoint(List<PointViewModel> listPoint, PointViewModel pointCheck)
        {
            int i, j;
            bool result = false;
            int countListPoint = listPoint.Count;

            // Ray-Casting
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
        /// <summary>
        /// Get PopupHtml by Code
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
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