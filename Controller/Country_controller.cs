using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class Country_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        //private Countries country;
        //public Country_controller()
        //{
        //    conn = new DbConnect().GetConnection();
        //}
        //public List<Countries> GetCountries()
        //{
        //    List<Countries> CountryList = new List<Countries>();
        //    try
        //    {
        //        string sql = "SELECT * FROM COUNTRIES";
        //        cmd = new SqlCommand(sql, conn);
        //        conn.Open();
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            int countryId = reader.GetInt32(reader.GetOrdinal("COUNTRY_ID"));
        //            string countrytName = reader.GetString(reader.GetOrdinal("COUNTRY_NAME"));
        //            // Tạo đối tượng Formats và thêm vào danh sách
        //            Countries country = new Countries(countryId, countrytName);
        //            CountryList.Add(country);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Lỗi: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (reader != null) reader.Close();
        //        if (conn != null) conn.Close();
        //    }
        //    return CountryList;
        //}
    }
}
