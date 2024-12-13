using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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

        public List<User_model> GetUserData()
        {
            List<User_model> users = new List<User_model>();
            string sql = "SELECT * FROM USERS WHERE STATUS='VERIFIED'";

            using (SqlConnection conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        User_model user = new User_model()
                        {
                            userId = reader["USER_ID"] != DBNull.Value ? Convert.ToInt32(reader["USER_ID"]) : 0,
                            username = reader["USERNAME"]?.ToString() ?? string.Empty,
                            email = reader["EMAIL"]?.ToString() ?? string.Empty,
                            gender = reader["GENDER"]?.ToString() ?? string.Empty,
                            birth = reader["BIRTH"] != DBNull.Value ? Convert.ToDateTime(reader["BIRTH"]) : DateTime.MinValue,
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }
        //Kiem tra dang nhap user
        public bool CheckLoginUser(User_model user, string password)
        {
            string sql = "SELECT * FROM USERS WHERE USERNAME = @username AND PASSWORD = @password AND STATUS = 'VERIFIED'";
            string hashedPassword = HashPassword(password);
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", user.username);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userId = Convert.ToInt32(reader["USER_ID"]);
                                user.userId = userId;
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        //Kiem tra dang ky
        public void CheckSignupUser(User_model user, string password)
        {
            try
            {
                if (CheckDuplicateUser(user.username))
                {
                    throw new Exception("Tên người dùng đã tồn tại.");
                }

                if (CheckDuplicateEmail(user.email))
                {
                    throw new Exception("Email đã tồn tại.");
                }

                string hashedPassword = HashPassword(password);
                string code = GenerateVerifyCode();
                string sql = "INSERT INTO USERS (USERNAME, EMAIL, PASSWORD, VERIFYCODE) " +
                             "OUTPUT INSERTED.USER_ID " +
                             "VALUES (@username, @email, @password, @verifycode)";

                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", user.username);
                        cmd.Parameters.AddWithValue("@email", user.email);
                        cmd.Parameters.AddWithValue("@password", hashedPassword);
                        cmd.Parameters.AddWithValue("@verifycode", code);

                        object result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            user.userId = Convert.ToInt32(result);
                        }
                        else
                        {
                            throw new Exception("Không thể lấy userID.");
                        }
                    }
                }

                user.verifyCode = code;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi đăng ký: {ex.Message}");
            }
        }

        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
        // Kiểm tra user đã tồn tại
        public bool CheckDuplicateUser(string username)
        {
            string sql = "SELECT * FROM USERS WHERE USERNAME = @username AND STATUS = 'VERIFIED'";

            using (SqlConnection conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    return cmd.ExecuteScalar() != null;
                }
            }
        }

        // Kiểm tra email đã tồn tại
        public bool CheckDuplicateEmail(string email)
        {
            string sql = "SELECT * FROM USERS WHERE EMAIL = @Email AND STATUS = 'VERIFIED'";

            using (SqlConnection conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    return cmd.ExecuteScalar() != null;
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
            string passwordRegex = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
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
            string query = "SELECT * FROM USERS WHERE VERIFYCODE = @code";

            using (SqlConnection conn = new DbConnect().GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@code", code);
                        object result = cmd.ExecuteScalar();
                        return result != null; 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }


        // Cập nhật status về "VERIFIED"
        public void DoneVerify(int userId)
        {
            string sql = "UPDATE USERS SET VERIFYCODE = '', STATUS = 'VERIFIED' WHERE USER_ID = @userId";
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@userId", userId);
                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            throw new Exception("Không thể xác minh người dùng.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // Kiểm tra verifyCode có đúng với userID cho không
        public bool VerifyCodeWithUser(int userID, string code)
        {
            string query = "SELECT 1 FROM USERS WHERE USER_ID = @userID AND VERIFYCODE = @code";

            using (SqlConnection conn = new DbConnect().GetConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@userID", userID);
                        cmd.Parameters.AddWithValue("@code", code);

                        // Sử dụng ExecuteScalar để lấy giá trị đầu tiên nếu tồn tại
                        object result = cmd.ExecuteScalar();
                        bool isVerified = result != null;

                        if (!isVerified)
                        {
                            MessageBox.Show("Không tìm thấy người dùng hoặc mã xác minh không chính xác.");
                        }

                        return isVerified;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }
        public void ForgotPassword(User_model user)
        {
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    string query = "SELECT USER_ID FROM USERS WHERE EMAIL = @Email AND STATUS = 'VERIFIED'";
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", user.email);

                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int userID = reader.GetInt32(0);
                                string code = GenerateVerifyCode();
                                user.userId = userID;
                                reader.Close();
                                string updateQuery = "UPDATE USERS SET VERIFYCODE = @VerifyCode WHERE USER_ID = @UserID";
                                using (var updateCommand = new SqlCommand(updateQuery, conn))
                                {
                                    updateCommand.Parameters.AddWithValue("@VerifyCode", code);
                                    updateCommand.Parameters.AddWithValue("@UserID", userID);
                                    updateCommand.ExecuteNonQuery();
                                }
                                user.verifyCode = code;
                            }
                            else
                            {
                                MessageBox.Show("Email không tồn tại hoặc chưa được xác minh.");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ResetPassword(string email, string password)
        {
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    string query = "UPDATE USERS SET PASSWORD = @Password WHERE EMAIL = @Email AND STATUS = 'VERIFIED'";
                    using (var command = new SqlCommand(query, conn))
                    {
                        string hashedPassword = HashPassword(password);
                        command.Parameters.AddWithValue("@Password", hashedPassword);
                        command.Parameters.AddWithValue("@Email", email);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public User_model GetInfo(int userId)
        {
            string sql = "SELECT USER_ID, USERNAME, GENDER, BIRTH, EMAIL FROM USERS WHERE USER_ID = @UserId";
            User_model userInfo = null;
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                userInfo = new User_model
                                {
                                    userId = reader.GetInt32(reader.GetOrdinal("USER_ID")),
                                    username = reader.IsDBNull(reader.GetOrdinal("USERNAME")) ? null : reader.GetString(reader.GetOrdinal("USERNAME")),
                                    email = reader.IsDBNull(reader.GetOrdinal("EMAIL")) ? null : reader.GetString(reader.GetOrdinal("EMAIL")),
                                    gender = reader.IsDBNull(reader.GetOrdinal("GENDER")) ? null : reader.GetString(reader.GetOrdinal("GENDER")),
                                    birth = reader.IsDBNull(reader.GetOrdinal("BIRTH")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("BIRTH"))
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return userInfo;
        }

        public bool UpdateInfo(User_model userModel)
        {
            string sql = "UPDATE USERS SET BIRTH = @Birth, GENDER = @Gender, EMAIL = @Email WHERE USER_ID = @UserId";
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Birth", userModel.birth);
                        cmd.Parameters.AddWithValue("@Gender", userModel.gender);
                        cmd.Parameters.AddWithValue("@Email", userModel.email);
                        cmd.Parameters.AddWithValue("@UserId", userModel.userId);

                        int rowsUpdated = cmd.ExecuteNonQuery();
                        return rowsUpdated > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        public bool DeleteData(int userId)
        {
            string deleteFavorite = "DELETE FROM FAVORITES WHERE MOVIE_ID IN (SELECT MOVIE_ID FROM MOVIES WHERE USER_ID = @userId)";
            string deleteUser = "DELETE FROM USERS WHERE USER_ID = @userId";

            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand(deleteFavorite, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@userId", userId);
                                cmd.ExecuteNonQuery();
                            }
                            using (SqlCommand cmd = new SqlCommand(deleteUser, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@userId", userId);
                                int row = cmd.ExecuteNonQuery();
                                if (row > 0)
                                {
                                    transaction.Commit();
                                    return true;
                                }
                                else
                                {
                                    transaction.Rollback();
                                    return false;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show(ex.Message);
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public bool DeleteData(int userId)
        {
            string sql = "DELETE FROM USERS WHERE USER_ID = @UserId";
            using (conn)
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
        }
    }
}
