using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class PhanHoiDanhGia_controller
    {
        private readonly SqlConnection conn;

        public PhanHoiDanhGia_controller()
        {
            conn = new DbConnect().GetConnection();
        }

        // Lấy thông tin phản hồi và đánh giá dựa trên ReviewId
        public (string Username, DateTime ReviewDate, int Rating, string Comment, string ReplyText, string title, string ImgPath) GetReviewDetails(int reviewId)
        {
            // Truy vấn theo cấu trúc mới mà bạn đã cung cấp
            string query = @"
        SELECT 
            u.USERNAME,
            r.REVIEW_DATE,
            r.RATING,
            r.COMMENT,
            r.REPLY,
            m.TITLE,
            m.COVER_IMAGE
        FROM 
            REVIEWS r
        LEFT JOIN 
            USERS u ON r.USER_ID = u.USER_ID
        LEFT JOIN 
            MOVIES m ON r.MOVIE_ID = m.MOVIE_ID
        WHERE 
            r.REVIEW_ID = @ReviewId";

            // Mở kết nối nếu chưa mở
            if (conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                // Thêm tham số để tránh SQL Injection
                cmd.Parameters.AddWithValue("@ReviewId", reviewId);

                // Thực thi truy vấn và lấy dữ liệu
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string username = reader.GetString(0); // USERNAME từ bảng USERS
                        DateTime reviewDate = reader.GetDateTime(1); // REVIEW_DATE từ bảng REVIEWS
                        int rating = reader.GetInt32(2); // RATING từ bảng REVIEWS
                        string comment = reader.GetString(3); // COMMENT từ bảng REVIEWS
                        string replyContent = reader.GetString(4); // REPLY từ bảng REVIEWS
                        string title = reader.GetString(5);
                        string imgPath = reader.IsDBNull(6) ? "" : reader.GetString(6); // COVER_IMAGE từ bảng MOVIES

                        return (username, reviewDate, rating, comment, replyContent, title, imgPath);
                    }
                }
            }

            // Đảm bảo đóng kết nối sau khi sử dụng
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }

            // Trả về giá trị mặc định nếu không tìm thấy dữ liệu
            return (null, DateTime.MinValue, 0, null, null, null, null);
        }


        // Thêm phản hồi mới
        public void AddReply(int reviewId, string replyContent)
        {
            // Truy vấn để cập nhật phản hồi cho một đánh giá
            string query = @"
                                UPDATE REVIEWS 
                                SET REPLY = @ReplyContent 
                                WHERE REVIEW_ID = @ReviewId";

            try
            {
                // Mở kết nối nếu chưa mở
                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    conn.Open();
                }

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Thêm tham số để tránh SQL Injection
                    cmd.Parameters.AddWithValue("@ReviewId", reviewId);
                    cmd.Parameters.AddWithValue("@ReplyContent", replyContent);

                    // Thực thi câu lệnh UPDATE
                    int rowsAffected = cmd.ExecuteNonQuery();

                    // Kiểm tra xem có cập nhật được dòng dữ liệu nào không
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Phản hồi đã được thêm thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy đánh giá để cập nhật.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm phản hồi: " + ex.Message);
            }
            finally
            {
                // Đảm bảo đóng kết nối sau khi sử dụng
                if (conn.State == System.Data.ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

    }
}
