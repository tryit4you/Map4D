using Map4D.Data.DAO;
using Map4D.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Map4D.Data.BO
{
    public class PolygonDetailBo
    {
        private PolygenDetailDao polygonDetail = new PolygenDetailDao();
        /// <summary>
        /// Get all City in VietNam
        /// </summary>
        /// <returns>List<CountriesViewModel></returns>
        public List<PolygonDetailViewModel> GetDetailByLatLng(string lat, string lng)
        {
            return polygonDetail.GetDetailByLatLng(lat, lng);
        }
    }
}