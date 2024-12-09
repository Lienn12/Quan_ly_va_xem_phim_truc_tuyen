using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class Format_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        private Format_model format;
        public Format_controller()
        {
            conn = new DbConnect().GetConnection();
        }
        public List<Format_model> GetFormats()
        {
            List<Format_model> formatList = new List<Format_model>();
            string sql = "SELECT * FROM FORMATS";
            try
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int formatId = reader.GetInt32(reader.GetOrdinal("FORMAT_ID"));
                    string formatName = reader.GetString(reader.GetOrdinal("FORMAT_NAME"));
                    // Tạo đối tượng Formats và thêm vào danh sách
                    Format_model format = new Format_model(formatId, formatName);
                    formatList.Add(format);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }
            finally
            {
                if (reader != null) reader.Close();
                if (conn != null) conn.Close();
            }
            return formatList;
        }
    }
}
