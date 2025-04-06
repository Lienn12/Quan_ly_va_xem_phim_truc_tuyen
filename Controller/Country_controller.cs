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
        public bool AddCountry(Country_model newCountry)
        {
            string sql = "INSERT INTO COUNTRIES (COUNTRY_NAME) VALUES (@CountryName)";
            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CountryName", newCountry.CountryName);
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return true; // Thêm thành công
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            return false; // Thêm thất bại
        }
        public bool UpdateCountry(Country_model updatedCountry)
        {
            string sql = "UPDATE COUNTRIES SET COUNTRY_NAME = @CountryName WHERE COUNTRY_ID = @CountryId";
            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CountryId", updatedCountry.CountryId);
                        cmd.Parameters.AddWithValue("@CountryName", updatedCountry.CountryName);
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            return true; // Sửa thành công
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            return false; // Sửa thất bại
        }
        public bool DeleteCountry(int countryId)
        {
            string checkSql = "SELECT COUNT(*) FROM MOVIES WHERE COUNTRY_ID = @CountryID";
            string deleteSql = "DELETE FROM COUNTRIES WHERE COUNTRY_ID = @CountryID";

            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(checkSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CountryID", countryId);
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Không thể xóa quốc gia vì đang được sử dụng bởi phim.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    using (cmd = new SqlCommand(deleteSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@CountryID", countryId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }
            finally
            {
                conn.Close();
            }
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
