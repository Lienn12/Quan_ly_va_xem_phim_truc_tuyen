using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Quan_ly_thu_vien_phim.Controller
{
    internal class User_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
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
                cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
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
        //Kiem tra dang nhap user
        public bool CheckLoginUser(User_model user, string password)
        {
            string sql = "SELECT * FROM USERS WHERE USERNAME=@username AND PASSWORD=@password";
            try
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@password",password);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return true;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return false;
        }
        //Kiem tra dang ky
        public void CheckSignupUser(User_model user, string password)
        {
            try
            {
                conn.Open ();
                string sql = "INSERT INTO USERS(USERNAME, EMAIL, PASSWORD,VERIFYCODE) VALUES (@username, @email,@password, @verifycode)";
                string code = GenerateVerifyCode();
                cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@username", user.username);
                cmd.Parameters.AddWithValue("@email", user.email);
                cmd.Parameters.AddWithValue("@password",password );
                cmd.Parameters.AddWithValue("@verifycode", code);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "SELECT SCOPE_IDENTITY()";
                object userID = cmd.ExecuteScalar();
                if (userID != null)
                {
                    user.userId = Convert.ToInt32(userID);
                }
                user.verifyCode = code;
            }
            catch (Exception ex)
            {
                MessageBox.Show (ex.Message);
                throw;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
        // Kiểm tra user đã tồn tại
        public bool CheckDuplicateUser(string username)
        {
            try
            {
                conn.Open();
                string query = "SELECT USER_ID FROM USERS WHERE USERNAME = @username AND STATUS = 'VERIFIED'";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                reader = cmd.ExecuteReader();
                bool exist = reader.HasRows;
                reader.Close();
                return exist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        // Kiểm tra email đã tồn tại
        public bool CheckDuplicateEmail(string email)
        {
            try
            {
                conn.Open();
                string query = "SELECT USER_ID FROM USERS WHERE EMAIL = @Email AND STATUS = 'VERIFIED'";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Email", email);
                reader = cmd.ExecuteReader();
                bool exist = reader.HasRows;
                reader.Close();
                return exist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        // Kiểm tra email đúng định dạng
        public bool CheckEmail(string email)
        {
            string emailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(email, emailRegex);
        }

        // Kiểm tra password đúng định dạng
        public bool CheckPassword(string password)
        {
            if (password.Length < 6)
            {
                return false;
            }
            string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d).+$";
            return Regex.IsMatch(password, passwordRegex);
        }

        // Kiểm tra confirmPassword có trùng với password
        public bool CheckConfirmPassword(string password, string confirmPassword)
        {
            return password.Equals(confirmPassword);
        }

        // Tạo verifyCode ngẫu nhiên
        private string GenerateVerifyCode()
        {
            Random random = new Random();
            string code;
            do
            {
                code = random.Next(0, 1000000).ToString("D6");
            } while (CheckDuplicateCode(code)); 

            return code;
        }

        // Kiểm tra verifyCode có trùng với userID không
        public bool CheckDuplicateCode(string code)
        {
            try
            {
                conn.Open();
                string query = "SELECT USER_ID FROM USERS WHERE VERIFYCODE = @code";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@code", code);

                reader = cmd.ExecuteReader();
                bool exist = reader.HasRows;

                reader.Close();
                return exist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        // Cập nhật status về "VERIFIED"
        public void DoneVerify(int userID)
        {
            try
            {
                conn.Open();
                string query = "UPDATE USERS SET VERIFYCODE = '', STATUS = 'VERIFIED' WHERE USER_ID = @userID";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userID", userID);

                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        // Kiểm tra verifyCode có đúng với userID cho không
        public bool VerifyCodeWithUser(int userID, string code)
        {
            try
            {
                conn.Open();
                string query = "SELECT USER_ID FROM USERS WHERE USER_ID = @userID AND VERIFYCODE = @code";
                cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@code", code);

                reader = cmd.ExecuteReader();
                bool exist = reader.HasRows;

                reader.Close();
                return exist;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }
    }
}
