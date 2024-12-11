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
        public List<DanhGia_model> GetReview()
        {
            List<DanhGia_model> dsReview = new List<DanhGia_model>();
            string sql = "SELECT REVIEW_ID, USERNAME, TITLE, REVIEWS.RATING, REVIEW_DATE FROM REVIEWS, USERS, MOVIES WHERE REVIEWS.MOVIE_ID = MOVIES.MOVIE_ID AND REVIEWS.USER_ID = USERS.USER_ID";
            using (SqlConnection conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DanhGia_model reviewModel = new DanhGia_model(reader);
                            dsReview.Add(reviewModel);
                        }
                    }
                }
            }
            return dsReview;
        }
        public DanhGia_model GetReply(int reviewId)
        {
            string sql = @"
                        SELECT Review_ID, USERNAME, REVIEW_DATE, REVIEWS.RATING, COMMENT, COVER_IMAGE, TITLE, RELEASE_YEAR, REPLY
                        FROM USERS, REVIEWS, MOVIES
                        WHERE REVIEWS.MOVIE_ID = MOVIES.MOVIE_ID 
                        AND REVIEWS.USER_ID = USERS.USER_ID 
                        AND REVIEW_ID = @reviewId";
            using (SqlConnection conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@reviewId", reviewId);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string username = reader["USERNAME"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("USERNAME")) : string.Empty;
                            DateTime reviewDate = reader["REVIEW_DATE"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("REVIEW_DATE")) : DateTime.MinValue;
                            int rating = reader["RATING"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("RATING")) : 0;
                            string comment = reader["COMMENT"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("COMMENT")) : string.Empty;
                            string img = reader["COVER_IMAGE"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("COVER_IMAGE")) : string.Empty;
                            string title = reader["TITLE"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("TITLE")) : string.Empty;
                            int releaseYear = reader["RELEASE_YEAR"] != DBNull.Value ? reader.GetInt32(reader.GetOrdinal("RELEASE_YEAR")) : 0;
                            string reply = reader["REPLY"] != DBNull.Value ? reader.GetString(reader.GetOrdinal("REPLY")) : string.Empty;

                            return new DanhGia_model(reviewId, username, reviewDate, rating, comment, img, title, releaseYear, reply);
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public bool SetReply(int reviewId, string reply)
        {
            string sql = "UPDATE REVIEWS SET REPLY = @reply WHERE REVIEW_ID = @reviewId";
            using (SqlConnection conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@reply", reply);
                    cmd.Parameters.AddWithValue("@reviewId", reviewId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
            
        }
        public bool DeleteReply(int reviewId)
        {
            string sql = "UPDATE reviews SET reply = '' WHERE review_ID = @reviewId";
            using (SqlConnection conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@reviewId", reviewId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
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
