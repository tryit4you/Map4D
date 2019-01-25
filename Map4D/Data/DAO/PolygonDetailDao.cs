using CommonLogger.Libraries;
using Map4D.Helper;
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
        /// <param name="Lat">Latitude</param>
        /// <param name="Lng">Longitude</param>
        /// <returns>InfoPointViewModel : City,District,Ward</returns>
        public InfoPointViewModel GetInfoPointByLatLngOptimize(string Lat, string Lng)
        {
            List<PolygonDetailViewModel> listDetail = GetDetailByLatLngOptimize(Lat, Lng);
            PointViewModel pointCheck = new PointViewModel() { Lng = double.Parse(Lng), Lat = double.Parse(Lat) };
            foreach (PolygonDetailViewModel polygon in listDetail)
            {
                List<PointViewModel> listPointTop = CalculatorHelper.ConvertObjectDataToListPoint(polygon.ObjectData);
                bool result = CheckWithListPoint(listPointTop, pointCheck);
                if (result)
                {
                    return GetInfoPointByWardCode(polygon.Value);
                }
            }
            return null;
        }
        public InfoPointViewModel GetInfoPointByLatLngUnOptimize(string Lat, string Lng)
        {
            List<PolygonDetailViewModel> listDetail = GetDetailByLatLngUnOptimize(Lat, Lng);
            PointViewModel pointCheck = new PointViewModel() { Lng = double.Parse(Lng), Lat = double.Parse(Lat) };
            foreach (PolygonDetailViewModel polygon in listDetail)
            {
                List<PointViewModel> listPointTop = CalculatorHelper.ConvertObjectDataToListPoint(polygon.ObjectData);
                bool result = CheckWithListPoint(listPointTop, pointCheck);
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
        /// <param name="wardCode">Ward Code</param>
        /// <returns>InfoPointViewModel : City,District,Ward</returns>
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
        /// <param name="Lat">Latitude</param>
        /// <param name="Lng">Longitude</param>
        /// <returns>List<PolygonDetailViewModel> : Id,ObjectData,Value</returns>
        private List<PolygonDetailViewModel> GetDetailByLatLngUnOptimize(string Lat, string Lng)
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
        private List<PolygonDetailViewModel> GetDetailByLatLngOptimize(string Lat, string Lng)
        {
            List<PolygonDetailViewModel> listPolygonDetails = new List<PolygonDetailViewModel>();
            string sqlQuery = "EXEC XacDinhToaDoOptimize @lat = @_Lat ,@lng = @_Lng";
            object[] _params = new object[] { new SqlParameter("_Lat", Lat), new SqlParameter("_Lng", Lng) };

            SqlDataReader reader = helper.ExecDataReader(sqlQuery, _params);
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
        /// <param name="wardCode">wardCode</param>
        /// <returns>CityName</returns>
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
        /// <param name="wardCode">WardCode</param>
        /// <returns>DistrictName</returns>
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
        /// <param name="wardCode">wardCode</param>
        /// <returns>WardName</returns>
        private string GetWardByWardCode(string wardCode)
        {
            string Ward = string.Empty;
            string sqlQuery = "SELECT Name FROM Countries WHERE Code = @wardCode";
            object[] _params = new object[] { new SqlParameter("wardCode", wardCode) };

            SqlDataReader reader = helper.ExecDataReader(sqlQuery,_params);
            if (reader.Read())
            {
                Ward = reader["Name"].ToString();
            }
            reader.Close();

            return Ward;
        }
        /// <summary>
        /// Check Exist Point In Polygon
        /// </summary>
        /// <param name="polygon">PolygonDetail: Id,ObjectData,Value</param>
        /// <param name="pointCheck">PointCheck exist in Polygon : Lat,Lng</param>
        /// <returns>True : Exist, False : Non-exist</returns>
      
        /// <summary>
        /// Convert ObjectData to ListPoint
        /// </summary>
        /// <param name="ObjectData">ObjectData for PolygonDetail</param>
        /// <returns>List<PointViewModel> : List vertices of Polygon</returns>
      
        /// <summary>
        /// Check Exitst with ListPoint
        /// </summary>
        /// <param name="listPoint">List vertices of Polygon</param>
        /// <param name="pointCheck">PointCheck exist in Polygon : Lat,Lng</param>
        /// <returns>True : Exist, False : Non-exist</returns>
        private bool CheckWithListPoint(List<PointViewModel> listPoint, PointViewModel pointCheck)
        {
            int i, j;
            bool result = false;
            int countListPoint = listPoint.Count;

            // Ray-Casting
            for (i = 0, j = countListPoint - 1; i < countListPoint; j = i++)
            {
                if (listPoint[i].Lng>=pointCheck.Lng)
                {
                    if (((listPoint[i].Lat > pointCheck.Lat) != (listPoint[j].Lat > pointCheck.Lat)) &&
                     (pointCheck.Lng < (listPoint[j].Lng - listPoint[i].Lng) * (pointCheck.Lat - listPoint[i].Lat) / (listPoint[j].Lat - listPoint[i].Lat) + listPoint[i].Lng))
                    {
                        result = !result;
                    }
                }
            }

            return result;
        }
        
        /// <summary>
        /// Get PopupHtml by Code
        /// </summary>
        /// <param name="Code">Code of City or District or Ward</param>
        /// <returns>Html of Popup Info : Country,City,District,Ward</returns>
        public string GetPopupHtmlByCode(string Code)
        {
            string html = "<div class='toggle-detail-property'><span class='glyphicon glyphicon-remove' id='close-popup'></span></div><div class='detail-property-header'><span>Thông tin</span></div><div class='detail-property-content'><div class='section section-info'><div class='item' data-code='null'><table class='table'><thead><tr><td colspan='2' style='font-weight:bold;'><span class='glyphicon glyphicon-tag'></span> Thông tin sơ lược</td></tr></thead><tbody><tr><td>Quốc gia</td><td>Việt Nam</td></tr>";
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
            return html + "</tbody></table></div></div></div>";
        }
    }
}