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
        /// <param name="wardCode"></param>
        /// <returns></returns>
        public InfoPointViewModel GetInfoPointByWardCode(string wardCode)
        {
            return polygonDetailDao.GetInfoPointByWardCode(wardCode);
        }
        /// <summary>
        /// Get InfoPoint by LatLng
        /// </summary>
        /// <param name="Lat"></param>
        /// <param name="Lng"></param>
        /// <returns></returns>
        public InfoPointViewModel GetInfoPointByLatLng(string Lat, string Lng)
        {
            return polygonDetailDao.GetInfoPointByLatLng(Lat, Lng);
        }
        /// <summary>
        /// Get PopupHtml by Code
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string GetPopupHtmlByCode(string Code)
        {
            return polygonDetailDao.GetPopupHtmlByCode(Code);
        }
    }
}