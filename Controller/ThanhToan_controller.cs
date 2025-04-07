using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class ThanhToan_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        public ThanhToan_controller()
        {
            conn = new DbConnect().GetConnection();
        }
        public List<ThanhToan_model> GetAllThanhToan()
        {
            List<ThanhToan_model> thanhToans = new List<ThanhToan_model>();


            using (conn = new DbConnect().GetConnection())
            {
                string sql = @"SELECT o.order_id, u.user_id, u.user_name, p.plan_id, p.plan_name, o.amount, o.payment_status, o.order_date
                         FROM Orders o
                         JOIN Users u ON o.user_id = u.user_id
                         JOIN Subscription_Plans p ON o.plan_id = p.plan_id";
                using (cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    using (reader = cmd.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            User_model user = new User_model
                            {
                                userId = (int)reader["user_id"],
                                username = reader["user_name"].ToString()
                            };
                            GoiDichVu_model plan = new GoiDichVu_model
                            {
                                PlanId = (int)reader["plan_id"],
                                PlanName = reader["plan_name"].ToString()
                            };

                            ThanhToan_model thanhToan = new ThanhToan_model
                            {
                                OrderId = (int)reader["order_id"],
                                User = user,
                                Plan = plan,
                                Amount = (decimal)reader["amount"],
                                PaymentStatus = reader["payment_status"].ToString(),
                                OrderDate = (DateTime)reader["order_date"]
                            };
                            thanhToans.Add(thanhToan);
                        }
                    }
                }
            }
            return thanhToans;
        }
        public ThanhToan_model GetThanhToanByUserId(int userId)
        {
            ThanhToan_model thanhToan = null;

            using (conn = new DbConnect().GetConnection())
            {
                string sql = @"SELECT o.order_id, u.user_id, u.username, p.plan_id, p.plan_name, p.price, p.duration_days, o.amount, o.payment_status, o.order_date
                     FROM Orders o
                     JOIN Users u ON o.user_id = u.user_id
                     JOIN Subscription_Plans p ON o.plan_id = p.plan_id
                     WHERE u.user_id = @UserId"; 

                using (cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId); 
                    conn.Open();

                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) // Chỉ lấy 1 kết quả đầu tiên
                        {
                            User_model user = new User_model
                            {
                                userId = (int)reader["user_id"],
                                username = reader["username"].ToString()
                            };
                            GoiDichVu_model plan = new GoiDichVu_model
                            {
                                PlanId = (int)reader["plan_id"],
                                PlanName = reader["plan_name"].ToString(),
                                Price= (decimal)reader["price"],
                                DurationDays= (int)reader["duration_days"]

                            };

                            thanhToan = new ThanhToan_model
                            {
                                OrderId = (int)reader["order_id"],
                                User = user,
                                Plan = plan,
                                Amount = (decimal)reader["amount"],
                                PaymentStatus = reader["payment_status"].ToString(),
                                OrderDate = (DateTime)reader["order_date"]
                            };
                        }
                    }
                }
            }
            return thanhToan;
        }

        public bool CreateOrderByUserId(int userId, int planId, int methodId, decimal amount)
        {
            using (conn = new DbConnect().GetConnection())
            {
                string sql = @"INSERT INTO Orders (user_id, plan_id, method_id, amount, payment_status, order_date) 
                       VALUES (@UserId, @PlanId, @MethodId, @Amount,  'completed', GETDATE())";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@PlanId", planId);
                    cmd.Parameters.AddWithValue("@MethodId", methodId);
                    cmd.Parameters.AddWithValue("@Amount", amount);

                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; 
                }
            }
        }


    }

}

