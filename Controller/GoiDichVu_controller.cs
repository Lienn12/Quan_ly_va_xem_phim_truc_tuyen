using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using Quan_ly_thu_vien_phim.Model;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class GoiDichVu_controller
    {
        private SqlConnection conn;
        private SqlCommand cmd;
        private SqlDataReader reader;
        public GoiDichVu_controller()
        {
            conn = new DbConnect().GetConnection();
        }

        // Lấy danh sách gói dịch vụ
        public List<GoiDichVu_model> GetPlans()
        {
            List<GoiDichVu_model> planList = new List<GoiDichVu_model>();
            string sql = "SELECT * FROM Subscription_Plans";

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
                                // Kiểm tra các giá trị có hợp lệ không
                                int plan_id = reader.GetInt32(reader.GetOrdinal("plan_id"));
                                string planName = reader.IsDBNull(reader.GetOrdinal("plan_name")) ? string.Empty : reader.GetString(reader.GetOrdinal("plan_name"));
                                decimal price = reader.IsDBNull(reader.GetOrdinal("price")) ? 0 : reader.GetDecimal(reader.GetOrdinal("price"));
                                int durationDays = reader.IsDBNull(reader.GetOrdinal("duration_days")) ? 0 : reader.GetInt32(reader.GetOrdinal("duration_days"));
                                string description = reader.IsDBNull(reader.GetOrdinal("description")) ? string.Empty : reader.GetString(reader.GetOrdinal("description"));

                                planList.Add(new GoiDichVu_model(plan_id, planName, price, durationDays, description));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return planList;
        }


        // Thêm mới gói dịch vụ
        public bool AddPlan(GoiDichVu_model newPlan)
        {
            if (newPlan == null)
            {
                MessageBox.Show("Gói dịch vụ không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            string sql = "INSERT INTO Subscription_Plans (plan_name, price, duration_days, description) VALUES (@Plan_Name, @Price, @Duration_Days, @Description)";

            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Plan_Name", newPlan.PlanName);
                        cmd.Parameters.AddWithValue("@Price", newPlan.Price);
                        cmd.Parameters.AddWithValue("@Duration_Days", newPlan.DurationDays);
                        cmd.Parameters.AddWithValue("@Description", newPlan.Description);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm gói dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }


        // Cập nhật gói dịch vụ
        public bool UpdatePlan(GoiDichVu_model updatedPlan)
        {
            string sql = "UPDATE Subscription_Plans SET Plan_Name = @Plan_Name, Price = @Price, Duration_Days = @Duration_Days, Description = @Description WHERE plan_id = @plan_id";

            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@plan_id", updatedPlan.PlanId);
                        cmd.Parameters.AddWithValue("@Plan_Name", updatedPlan.PlanName);
                        cmd.Parameters.AddWithValue("@Price", updatedPlan.Price);
                        cmd.Parameters.AddWithValue("@Duration_Days", updatedPlan.DurationDays);
                        cmd.Parameters.AddWithValue("@Description", updatedPlan.Description);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0; // Nếu cập nhật thành công, trả về true
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật gói dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }

        // Xóa gói dịch vụ
        public bool DeletePlan(int plan_id)
        {
            string sql = "DELETE FROM Subscription_Plans WHERE plan_id = @plan_id";

            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@plan_id", plan_id);

                        int result = cmd.ExecuteNonQuery();
                        return result > 0; // Nếu xóa thành công, trả về true
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa gói dịch vụ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;
        }
    }
}
