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
    public class PhuongThuc_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        private PhuongThuc_model Payment_Methods;
        public PhuongThuc_controller()
        {
            conn = new DbConnect().GetConnection();
        }
        public List<PhuongThuc_model> GetMethods()
        {
            List<PhuongThuc_model> methodList = new List<PhuongThuc_model>();
            string sql = "SELECT * FROM Payment_Methods";
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
                                int methodId = reader.GetInt32(reader.GetOrdinal("METHOD_ID"));
                                string methodName = reader.GetString(reader.GetOrdinal("METHOD_NAME"));
                                methodList.Add(new PhuongThuc_model(methodId, methodName));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            return methodList;
        }
        public bool AddMethod(PhuongThuc_model newMethod)
        {
            string sql = "INSERT INTO Payment_Methods (METHOD_NAME) VALUES (@MethodName)";
            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MethodName", newMethod.MethodName);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        public bool UpdateMethod(PhuongThuc_model updatedMethod)
        {
            string sql = "UPDATE Payment_Methods SET METHOD_NAME = @MethodName WHERE METHOD_ID = @MethodId";
            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MethodId", updatedMethod.MethodId);
                        cmd.Parameters.AddWithValue("@MethodName", updatedMethod.MethodName);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
        public bool DeleteMethod(int methodId)
        {
            string sql = "DELETE FROM Payment_Methods WHERE METHOD_ID = @MethodId";
            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@MethodId", methodId);
                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

    }
}
