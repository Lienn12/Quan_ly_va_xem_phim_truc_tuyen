using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Quan_ly_thu_vien_phim.Controller
{
    public class DbConnect
    {
        public SqlConnection GetConnection()
        {

//             string sql = "Data Source=HANNIEL; Initial Catalog=Thu_vien_phim; Integrated Security=true;";
            string sql = "Data Source=LAPTOP-7F0OPAJM\\LIEN; Initial Catalog=Quan_ly_thu_vien_phim; Integrated Security=true;";

            SqlConnection conn = new SqlConnection(sql);
            if (conn != null)
            {
                Console.WriteLine("Thanh cong");
            }
            else {
                Console.WriteLine("That bai");
            }
            return conn;
        }
        
    }
}
