using Map4D.Data.DAO;
using Map4D.ViewModels;
using System.Collections.Generic;

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
        public List<CountriesViewModel> GetAllDistrict(int level)
        {
            return drawPolygonDao.GetByLevel(level);
        }
        public List<CountriesViewModel> GetAllWard(int level)
        {
            return drawPolygonDao.GetByLevel(level);
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
        /// Get ObjectData by Code
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string GetObjectDataByCode(string Value)
        {
            return drawPolygonDao.GetObjectDataByCode(Value);
        }
        /// <summary>
        /// Get PointCenter by Code
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public PointViewModel GetPointCenterByCode(string Code)
        {
            return drawPolygonDao.GetPointCenterByCode(Code);
        }
        public List<CountriesViewModel> GetAllData()
        {
            return drawPolygonDao.GetAllData();
        }
    }
}