using Map4D.Data.DAO;
using Map4D.ViewModels;

namespace Map4D.Data.BO
{
    public class PolygonDetailBo
    {
        private PolygonDetailDao polygonDetailDao = new PolygonDetailDao();
        /// <summary>
        /// Get InfoPoint by WardCode
        /// </summary>
        /// <param name="wardCode">Ward Code</param>
        /// <returns>InfoPointViewModel : City,District,Ward</returns>
        public InfoPointViewModel GetInfoPointByWardCode(string wardCode)
        {
            return polygonDetailDao.GetInfoPointByWardCode(wardCode);
        }
        /// <summary>
        /// Get InfoPoint by LatLng
        /// </summary>
        /// <param name="Lat">Latitude</param>
        /// <param name="Lng">Longitude</param>
        /// <returns>InfoPointViewModel : City,District,Ward</returns>
        public InfoPointViewModel GetInfoPointByLatLngOptimize(string Lat, string Lng)
        {
            return polygonDetailDao.GetInfoPointByLatLngOptimize(Lat, Lng);
        }
        public InfoPointViewModel GetInfoPointByLatLngUnOptimize(string Lat, string Lng)
        {
            return polygonDetailDao.GetInfoPointByLatLngUnOptimize(Lat, Lng);
        }
        /// <summary>
        /// Get PopupHtml by Code
        /// </summary>
        /// <param name="Code">Code of City or District or Ward</param>
        /// <returns>Html of Popup Info : Country,City,District,Ward</returns>
        public string GetPopupHtmlByCode(string Code)
        {
            return polygonDetailDao.GetPopupHtmlByCode(Code);
        }
    }
}