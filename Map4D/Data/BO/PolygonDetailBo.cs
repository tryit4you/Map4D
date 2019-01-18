using Map4D.Data.DAO;
using Map4D.Models;
using Map4D.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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