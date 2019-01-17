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
        public List<PolygonDetailViewModel> GetDetailByLatLng(string lat, string lng)
        {
            return polygonDetailDao.GetDetailByLatLng(lat, lng);
        }
        public bool KiemTraTonTai(PolygonDetailViewModel polygon, PointViewModel pointKiemTra)
        {
            return polygonDetailDao.KiemTraTonTai(polygon, pointKiemTra);
        }
        public bool KiemTraTonTai2(PolygonDetailViewModel polygon, PointViewModel pointKiemTra)
        {
            return polygonDetailDao.KiemTraTonTai2(polygon, pointKiemTra);
        }
    }
}