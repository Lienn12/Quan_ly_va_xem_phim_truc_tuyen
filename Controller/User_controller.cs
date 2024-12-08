using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Quan_ly_thu_vien_phim.Controller
{
    internal class User_controller
    {
        SqlConnection conn = null;
        SqlCommand command = null;
        SqlDataReader reader = null;
         public User_controller()
        {
            conn = new DbConnect().GetConnection();
        }

        public List<User_model> getUserData()
        {
            List<User_model> users = new List<User_model>();    
            string sql = $"SELECT * FROM USERS ";
            try
            {
                conn.Open();
                command = new SqlCommand(sql, conn);
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User_model user = new User_model()
                    {
                        userId = reader["User_Id"] != DBNull.Value ? Convert.ToInt32(reader["User_Id"]) : 0,
                        username = reader["Username"] != DBNull.Value ? reader["Username"].ToString() : string.Empty,
                        email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : string.Empty,
                        gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : string.Empty,
                        birth = reader["Birth"] != DBNull.Value ? Convert.ToDateTime(reader["Birth"]) : DateTime.MinValue,
                        verifyCode =reader["VerifyCode"] != DBNull.Value ? reader["VerifyCode"].ToString() : string.Empty
                    };
                    users.Add(user);                   
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error "+ ex.Message);
            }
            finally
            {
                if(reader != null) reader.Close();
                if(conn != null) conn.Close();
            }
            return users;
        }
    }
}
