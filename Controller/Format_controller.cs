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
        //private Formats format;
        //public Format_controller() 
        //{
        //    conn = new DbConnect().GetConnection();
        //}
        //public List<Formats> GetFormats()
        //{
        //    List<Formats> formatList = new List<Formats>();
        //    try
        //    {
        //        string sql = "SELECT * FROM Formats";
        //        cmd = new SqlCommand(sql, conn);
        //        conn.Open();
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            int formatId = reader.GetInt32(reader.GetOrdinal("FORMAT_ID"));
        //            string formatName = reader.GetString(reader.GetOrdinal("FORMAT_NAME"));
        //            // Tạo đối tượng Formats và thêm vào danh sách
        //            Formats format = new Formats(formatId, formatName);
        //            formatList.Add(format);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Lỗi: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if(reader != null) reader.Close();
        //        if(conn != null) conn.Close();
        //    }
        //    return formatList;
        //}
    }
}
