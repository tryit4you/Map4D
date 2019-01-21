using Map4D.Data.DAO;
using Map4D.Models;

namespace Map4D.Data.BO
{
    public class PolygonDetailBo
    {
        private PolygonDetailDao polygonDetailDao = new PolygonDetailDao();

        public InfoPointViewModel GetInfoPointByWardCode(string wardCode)
        {
            return polygonDetailDao.GetInfoPointByWardCode(wardCode);
        }

        public InfoPointViewModel GetInfoPointByLatLng(string Lat, string Lng)
        {
            return polygonDetailDao.GetInfoPointByLatLng(Lat, Lng);
        }

        public string GetPopupHtmlByCode(string Code)
        {
            return polygonDetailDao.GetPopupHtmlByCode(Code);
        }
    }
}