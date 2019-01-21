using CommonLogger.Libraries;
using Map4D.Models;
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
        /// Get all City in VietNam
        /// </summary>
        /// <returns>List<CountriesViewModel></returns>
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
        /// Get all District by City
        /// </summary>
        /// <param name="IdCity"></param>
        /// <returns>List<CountriesViewModel></returns>
        public List<CountriesViewModel> GetAllDistrictByCity(int IdCity)
        {
            List<CountriesViewModel> listDistrict = new List<CountriesViewModel>();
            string sqlQuery = "SELECT * FROM Countries WHERE ParentId = " + IdCity.ToString() + "  ORDER BY Name";
            SqlDataReader reader = helper.ExecDataReader(sqlQuery);
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
            string sqlQuery = "SELECT * FROM Countries WHERE ParentId = " + IdDistrict.ToString() + "  ORDER BY Name";
            SqlDataReader reader = helper.ExecDataReader(sqlQuery);
            while (reader.Read())
            {
                CountriesViewModel Ward = new CountriesViewModel
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
                listWard.Add(Ward);
            }
            reader.Close();
            return listWard;
        }

        public string GetDuLieuDoiTuongByCode(string Code)
        {
            string DuLieuDoiTuong = "[]";
            string sqlQuery = "EXEC GetDuLieuDoiTuongByCode @Code = '" + Code + "'";
            SqlDataReader reader = helper.ExecDataReader(sqlQuery);
            if (reader.Read())
            {
                DuLieuDoiTuong = reader["DuLieuDoiTuong"].ToString();
            }
            reader.Close();
            return DuLieuDoiTuong;
        }

        public PointViewModel GetPointCenterByCode(string Code)
        {
            PointViewModel pointCenter = null;
            string sqlQuery = "EXEC GetPointCenterByCode @Code = '" + Code + "'";
            SqlDataReader reader = helper.ExecDataReader(sqlQuery);
            if (reader.Read())
            {
                double Lat = double.Parse(reader["Lat"].ToString());
                double Lng = double.Parse(reader["Lng"].ToString());
                pointCenter = new PointViewModel() { Lat = Lat, Lng = Lng };
            }
            reader.Close();
            return pointCenter;
        }

        private List<string> GetListIdByCode(string code)
        {
            List<string> Ids = new List<string>();
            string queryListId = $"select Id from  ThongTinDoiTuongChinh WHERE DiaGioiHanhChinhCode={code}";
            SqlDataReader reader = helper.ExecDataReader(queryListId);
            while (reader.Read())
            {
                Ids.Add(reader["Id"].ToString());
            }
            reader.Close();
            return Ids;
        }

        public List<string> GetShapeByCode(string code)
        {
            List<string> shapes = new List<string>();
            var query = $"EXEC GetAllShapJsonByDiaGioiHanhChinhCode '{code}'";
            SqlDataReader reader = helper.ExecDataReader(query);
            while (reader.Read())
            {
                shapes.Add(reader["ObjectShape"].ToString());
            }
            reader.Close();

            return shapes;
        }
    }
}