using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class UserVip_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        public UserVip_controller()
        {
            conn = new DbConnect().GetConnection();
        }
        public UserVip_model GetVipByUserId(int userId)
        {
            UserVip_model userVip = null;

            string query = @"
                SELECT o.order_date,
                       u.user_id, u.username, u.email,
                       sp.plan_id, sp.plan_name, sp.price, sp.duration_days
                FROM Orders o
                JOIN Users u ON o.user_id = u.user_id
                JOIN Subscription_Plans sp ON o.plan_id = sp.plan_id
                WHERE o.user_id = @UserId AND o.payment_status = 'completed'
                ORDER BY o.order_date DESC"; // Lấy đơn mới nhất

            using (conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            DateTime startDate = Convert.ToDateTime(reader["order_date"]);
                            int duration = Convert.ToInt32(reader["duration_days"]);
                            DateTime endDate = startDate.AddDays(duration);

                            // Lấy thông tin user
                            User_model user = new User_model
                            {
                                userId = Convert.ToInt32(reader["user_id"]),
                                username = reader["username"].ToString(),
                                email = reader["email"].ToString()
                            };

                            // Lấy thông tin gói
                            GoiDichVu_model plan = new GoiDichVu_model
                            {
                                PlanId = Convert.ToInt32(reader["plan_id"]),
                                PlanName = reader["plan_name"].ToString(),
                                Price = Convert.ToDecimal(reader["price"]),
                                DurationDays = duration
                            };

                            // Gán vào model VIP
                            userVip = new UserVip_model
                            {
                                SubscriptionId = 0, // Nếu không cần SubscriptionId thì để 0 hoặc bỏ
                                user = user,
                                plan = plan,
                                StartDate = startDate,
                                EndDate = endDate,
                                IsActive = DateTime.Now < endDate
                            };
                        }
                    }
                }
            }

            return userVip;
        }
        public bool IsUserVip(int userId)
        {
            var vip = GetVipByUserId(userId);
            return vip != null && vip.IsActive;
        }
        public bool CancelVip(int userId)
        {
            string query = @"
                WITH LatestOrder AS (
                    SELECT TOP 1 * 
                    FROM Orders 
                    WHERE user_id = @UserId AND payment_status = 'completed'
                    ORDER BY order_date DESC
                )
                UPDATE LatestOrder 
                SET payment_status = 'cancelled'";

                    using (conn = new DbConnect().GetConnection())
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
        }
        public bool AddSubscription(int userId, int planId)
        {
            string query = @"
        DECLARE @Duration INT;

        SELECT @Duration = duration_days FROM Subscription_Plans WHERE plan_id = @PlanId;

        INSERT INTO User_Subscriptions (user_id, plan_id, start_date, end_date, is_active)
        VALUES (
            @UserId,
            @PlanId,
            GETDATE(),
            DATEADD(DAY, @Duration, GETDATE()),
            1
        );
    ";

            using (conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@PlanId", planId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }

    }
}
