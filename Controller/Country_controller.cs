using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class Country_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        private Country_model country;
        public Country_controller()
        {
            conn = new DbConnect().GetConnection();
        }
        public List<Country_model> GetCountries()
        {
            List<Country_model> CountryList = new List<Country_model>();
            string sql = "SELECT * FROM COUNTRIES";
            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        using (reader = cmd.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                int countryId = reader.GetInt32(reader.GetOrdinal("COUNTRY_ID"));
                                string countrytName = reader.GetString(reader.GetOrdinal("COUNTRY_NAME"));
                                // Tạo đối tượng Formats và thêm vào danh sách
                                Country_model country = new Country_model(countryId, countrytName);
                                CountryList.Add(country);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            return CountryList;
        }
    }
}
