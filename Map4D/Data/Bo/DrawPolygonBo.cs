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
        public List<CountriesViewModel> GetAllDistrict()
        {
            return drawPolygonDao.GetByLevel(2);
        }
        public List<CountriesViewModel> GetAllWard()
        {
            return drawPolygonDao.GetByLevel(3);
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
        /// <param name="Code">CityCode or DistrictCode or WardCode</param>
        /// <returns>string Json ObjectData</returns>
        public string GetObjectDataByCode(string Value)
        {
            return drawPolygonDao.GetObjectDataByCode(Value);
        }
        /// <summary>
        /// Get PointCenter by Code
        /// </summary>
        /// <param name="Code">CityCode or DistrictCode or WardCode</param>
        /// <returns>PointCenter of Pylogon: Lat,Lngs</returns>
        public PointViewModel GetPointCenterByCode(string Code)
        {
            return drawPolygonDao.GetPointCenterByCode(Code);
        }
        /// <summary>
        /// Get allData in VietNam
        /// </summary>
        /// <returns>List<CountriesViewModel> : All Data in VietNam</returns>
        public List<CountriesViewModel> GetAllData()
        {
            return drawPolygonDao.GetAllData();
        }
    }
}