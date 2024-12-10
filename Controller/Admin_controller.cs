using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class Admin_controller
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        public bool CheckLoginAdmin(Admin_model admin, string password)
        {
            string sql = "SELECT * FROM ADMIN WHERE USERNAME = @username AND PASSWORD = @password ";
            string hashedPassword = HashPassword(password);

            using (SqlConnection conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", admin.Username);
                    cmd.Parameters.AddWithValue("@password", hashedPassword);
                    return cmd.ExecuteScalar() != null;
                }
            }
        }

        public void CheckSignupAdmin(Admin_model admin, string password)
        {
            try
            {
                string hashedPassword = HashPassword(password);
                string sql = "INSERT INTO ADMIN (USERNAME,PASSWORD) VALUES (@username, @password)";

                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", admin.Username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        int rowsInserted = cmd.ExecuteNonQuery();

                        if (rowsInserted > 0)
                        {
                            Console.WriteLine("Thêm admin thành công.");
                        }
                        else
                        {
                            throw new Exception("Không thể thêm admin vào cơ sở dữ liệu.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi đăng ký: {ex.Message}");
            }
        }
        
        

    }
}
