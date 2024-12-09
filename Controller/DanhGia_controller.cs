using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Controller
{
    internal class DanhGia_controller
    {
        private readonly SqlConnection conn;

        public DanhGia_controller()
        {
            conn = new DbConnect().GetConnection();
        }

        public List<DanhGia_model> getdanhgia()
        {
            List<DanhGia_model> reviews = new List<DanhGia_model>();

            try
            {
                // Câu truy vấn SQL
                string query = @"SELECT 
                                    REVIEWS.REVIEW_ID AS 'ID Đánh Giá',
                                    USERS.USERNAME AS 'Tên Tài Khoản',
                                    MOVIES.TITLE AS 'Tên Phim',
                                    REVIEWS.RATING AS 'Rating',
                                    FORMAT(REVIEWS.REVIEW_DATE, 'dd/MM/yyyy HH:mm:ss') AS 'Ngày Đánh Giá'
                                FROM REVIEWS
                                INNER JOIN USERS ON REVIEWS.USER_ID = USERS.USER_ID
                                INNER JOIN MOVIES ON REVIEWS.MOVIE_ID = MOVIES.MOVIE_ID;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Thêm từng dòng dữ liệu vào danh sách
                            DanhGia_model review = new DanhGia_model
                            {
                                ReviewId = Convert.ToInt32(reader["ID Đánh Giá"]),
                                Username = reader["Tên Tài Khoản"].ToString(),
                                MovieTitle = reader["Tên Phim"].ToString(),
                                Rating = Convert.ToInt32(reader["Rating"]),
                                ReviewDate = reader["Ngày Đánh Giá"].ToString()
                            };

                            reviews.Add(review);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi lỗi nếu xảy ra
                Console.WriteLine("Lỗi khi lấy dữ liệu đánh giá: " + ex.Message);
            }
            finally
            {
                // Đảm bảo kết nối được đóng
                if (conn.State == System.Data.ConnectionState.Open)
                    conn.Close();
            }

            return reviews;
        }
        public void DeleteReview(int reviewId)
        {
            using (SqlConnection conn = new DbConnect().GetConnection())
            {
                string query = "DELETE FROM REVIEWS WHERE REVIEW_ID = @ReviewId";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ReviewId", reviewId);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

    }
}
