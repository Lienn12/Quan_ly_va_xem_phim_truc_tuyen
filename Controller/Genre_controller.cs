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
    public class Genre_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        public Genre_controller()
        {
            conn = new DbConnect().GetConnection();
        }
        public List<Genre_model> GetGenres()
        {
            List<Genre_model> GenreList = new List<Genre_model>();
            string sql = "SELECT * FROM GENRES";
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
                                int genreId = reader.GetInt32(reader.GetOrdinal("GENRE_ID"));
                                string genreName = reader.GetString(reader.GetOrdinal("GENRE_NAME"));
                                Genre_model genre = new Genre_model(genreId, genreName);
                                GenreList.Add(genre);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            return GenreList;
        }
         public bool AddGenre(Genre_model newGenre)
        {
            string sql = "INSERT INTO GENRES (GENRE_NAME) VALUES (@GenreName)";
            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@GenreName", newGenre.GenreName);
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

        // Sửa thể loại
        public bool UpdateGenre(Genre_model updatedGenre)
        {
            string sql = "UPDATE GENRES SET GENRE_NAME = @GenreName WHERE GENRE_ID = @GenreId";
            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@GenreId", updatedGenre.GenreID);
                        cmd.Parameters.AddWithValue("@GenreName", updatedGenre.GenreName);
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

        public bool DeleteGenre(int genreId)
        {
            string checkSql = "SELECT COUNT(*) FROM MOVIES WHERE GENRE_ID = @GenreID";
            string deleteSql = "DELETE FROM GENRES WHERE GENRE_ID = @GenreID";

            try
            {
                using (conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(checkSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@GenreID", genreId);
                        int count = (int)cmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Không thể xóa thể loại vì đang được sử dụng bởi phim.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }
                    using (cmd = new SqlCommand(deleteSql, conn))
                    {
                        cmd.Parameters.AddWithValue("@GenreID", genreId);
                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                return false;
            }

        }

    }
}
