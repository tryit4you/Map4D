using CommonLogger.Libraries;
using Map4D.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Map4D.Data.DAO
{
    public class DrawPolygonDao : AdoHelper
    {
        private AdoHelper helper = null;
        /// <summary>
        /// Construct Open SqlConnection
        /// </summary>
        public DrawPolygonDao()
        {
            string connection = AdoHelper.ConnectionString;
            helper = new AdoHelper(connection);
        }
        /// <summary>
        /// Get allData in VietNam
        /// </summary>
        /// <returns>List<CountriesViewModel> : All Data in VietNam</returns>
        public List<CountriesViewModel> GetAllData()
        {
            List<CountriesViewModel> allData = new List<CountriesViewModel>();
            string sqlQuery = "SELECT * FROM Countries ORDER BY Level";

            SqlDataReader reader = helper.ExecDataReader(sqlQuery);
            while (reader.Read())
            {
                CountriesViewModel Data = new CountriesViewModel
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Description = reader["Description"].ToString(),
                    Level = int.Parse(reader["Level"].ToString()),
                    Type = reader["Type"].ToString(),
                    ParentId = int.Parse(reader["ParentId"].ToString()),
                    IsVisible = int.Parse(reader["IsVisible"].ToString()),
                    IsState = bool.Parse(reader["IsState"].ToString()),
                    NameKhongDau = reader["NameKhongDau"].ToString()
                };
                allData.Add(Data);
            }
            reader.Close();

            return allData;
        }
        /// <summary>
        /// Get all City in VietNam
        /// </summary>
        /// <returns>List<CountriesViewModel>:all City in VietNam</returns>
        public List<CountriesViewModel> GetAllCity()
        {
            List<CountriesViewModel> listCity = new List<CountriesViewModel>();
            string sqlQuery = "SELECT * FROM Countries WHERE Level = 1 ORDER BY Name";

            SqlDataReader reader = helper.ExecDataReader(sqlQuery);
            while (reader.Read())
            {
                CountriesViewModel City = new CountriesViewModel
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Description = reader["Description"].ToString(),
                    Level = int.Parse(reader["Level"].ToString()),
                    Type = reader["Type"].ToString(),
                    ParentId = int.Parse(reader["ParentId"].ToString()),
                    IsVisible = int.Parse(reader["IsVisible"].ToString()),
                    IsState = bool.Parse(reader["IsState"].ToString()),
                    NameKhongDau = reader["NameKhongDau"].ToString()
                };
                listCity.Add(City);
            }
            reader.Close();

            return listCity;
        }
        /// <summary>
        /// Get Countries by Level
        /// </summary>
        /// <param name="level">int level</param>
        /// <returns>List Countries by Level</returns>
        public List<CountriesViewModel> GetByLevel(int level)
        {
            List<CountriesViewModel> listByLevel = new List<CountriesViewModel>();
            string sqlQuery = $"SELECT * FROM Countries WHERE Level = {level}";
            SqlDataReader reader = helper.ExecDataReader(sqlQuery);
            while (reader.Read())
            {
                CountriesViewModel data = new CountriesViewModel
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Name = reader["Name"].ToString(),
                    Code = reader["Code"].ToString(),
                    Description = reader["Description"].ToString(),
                    Level = int.Parse(reader["Level"].ToString()),
                    Type = reader["Type"].ToString(),
                    ParentId = int.Parse(reader["ParentId"].ToString()),
                    IsVisible = int.Parse(reader["IsVisible"].ToString()),
                    IsState = bool.Parse(reader["IsState"].ToString()),
                    NameKhongDau = reader["NameKhongDau"].ToString()
                };
                listByLevel.Add(data);
            }
            reader.Close();
            return listByLevel;
        }
        /// <summary>
        /// Get all District by City
        /// </summary>
        /// <param name="IdCity"></param>
        /// <returns>List<CountriesViewModel></returns>
        public List<CountriesViewModel> GetAllDistrictByCity(int IdCity)
        {
            List<CountriesViewModel> listDistrict = new List<CountriesViewModel>();
            string sqlQuery = "SELECT * FROM Countries WHERE ParentId = @ParentId ORDER BY Name";
            object[] _params = new object[] { new SqlParameter("ParentId", IdCity.ToString()) };

            SqlDataReader reader = helper.ExecDataReader(sqlQuery,_params);
            while (reader.Read())
            {
                CountriesViewModel District = new CountriesViewModel
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Code = reader["Code"].ToString(),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    Level = int.Parse(reader["Level"].ToString()),
                    Type = reader["Type"].ToString(),
                    ParentId = int.Parse(reader["ParentId"].ToString()),
                    IsVisible = int.Parse(reader["IsVisible"].ToString()),
                    IsState = bool.Parse(reader["IsState"].ToString()),
                    NameKhongDau = reader["NameKhongDau"].ToString()
                };
                listDistrict.Add(District);
            }
            reader.Close();

            return listDistrict;
        }
        /// <summary>
        /// Get all Ward by District
        /// </summary>
        /// <param name="IdDistrict"></param>
        /// <returns>List<CountriesViewModel></returns>
        public List<CountriesViewModel> GetAllWardByDistrict(int IdDistrict)
        {
            List<CountriesViewModel> listWard = new List<CountriesViewModel>();
            string sqlQuery = "SELECT * FROM Countries WHERE ParentId = @ParentId ORDER BY Name";
            object[] _params = new object[] { new SqlParameter("ParentId", IdDistrict.ToString()) };

            SqlDataReader reader = helper.ExecDataReader(sqlQuery,_params);
            while (reader.Read())
            {
                CountriesViewModel Ward = new CountriesViewModel
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Code=reader["Code"].ToString(),
                    Name = reader["Name"].ToString(),
                    Description = reader["Description"].ToString(),
                    Level = int.Parse(reader["Level"].ToString()),
                    Type = reader["Type"].ToString(),
                    ParentId = int.Parse(reader["ParentId"].ToString()),
                    IsVisible = int.Parse(reader["IsVisible"].ToString()),
                    IsState = bool.Parse(reader["IsState"].ToString()),
                    NameKhongDau = reader["NameKhongDau"].ToString()
                };
                listWard.Add(Ward);
            }
            reader.Close();

            return listWard;
        }
        /// <summary>
        /// Get ObjectData by Code
        /// </summary>
        /// <param name="Code">CityCode or DistrictCode or WardCode</param>
        /// <returns>string Json ObjectData</returns>
        public string GetObjectDataByCode(string Code)
        {
            string objectData = "[]";
            string sqlQuery = "EXEC GetDuLieuDoiTuongByCode @Code = @_Code";
            object[] _params = new object[] { new SqlParameter("_Code", Code) };

            SqlDataReader reader = helper.ExecDataReader(sqlQuery,_params);
            if (reader.Read())
            {
                objectData = reader["DuLieuDoiTuong"].ToString();
            }
            reader.Close();

            return objectData;
        }
        /// <summary>
        /// Get PointCenter by Code
        /// </summary>
        /// <param name="Code">CityCode or DistrictCode or WardCode</param>
        /// <returns>PointCenter of Pylogon: Lat,Lngs</returns>
        public PointViewModel GetPointCenterByCode(string Code)
        {
            PointViewModel pointCenter = null;
            string sqlQuery = "EXEC GetPointCenterByCode @Code = @_Code";
            object[] _params = new object[] { new SqlParameter("_Code", Code) };

            SqlDataReader reader = helper.ExecDataReader(sqlQuery,_params);
            if (reader.Read())
            {
                double Lat = double.Parse(reader["Lat"].ToString());
                double Lng = double.Parse(reader["Lng"].ToString());
                pointCenter = new PointViewModel() { Lat = Lat, Lng = Lng };
            }
            reader.Close();

            return pointCenter;
        }
    }
}