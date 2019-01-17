using Map4D.Data.DAO;
using Map4D.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Map4D.Data.BO
{
    public class DrawPolygonBo
    {
        private DrawPolygonDao drawPolygonDao = new DrawPolygonDao();
        /// <summary>
        /// Get all City in VietNam
        /// </summary>
        /// <returns>List<CountriesViewModel></returns>
        public List<CountriesViewModel> GetAllCity()
        {
            return drawPolygonDao.GetAllCity();
        }
        /// <summary>
        /// Get all District by City
        /// </summary>
        /// <param name="IdCity"></param>
        /// <returns>List<CountriesViewModel></returns>
        public List<CountriesViewModel> GetAllDistrictByCity(int IdCity)
        {
            return drawPolygonDao.GetAllDistrictByCity(IdCity);
        }
        /// <summary>
        /// Get all Ward by District
        /// </summary>
        /// <param name="IdDistrict"></param>
        /// <returns>List<CountriesViewModel></returns>
        public List<CountriesViewModel> GetAllWardByDistrict(int IdDistrict)
        {
            return drawPolygonDao.GetAllWardByDistrict(IdDistrict);
        }
        /// <summary>
        /// Get all ShapeByCode
        /// </summary>
        /// <param name="code"></param>
        /// <returns>List<CountriesViewModel></returns>
        public List<string> GetAllShapesByCode(string code)
        {
            return drawPolygonDao.GetShapeByCode(code);
        }
        /// <summary>
        /// Get dữ liệu đối tượng từ Value thông tin đối tượng phụ
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string getDuLieuDoiTuongByCode(string Value)
        {
            return provinceDao.getDuLieuDoiTuongByCode(Value);
        }
    }
}