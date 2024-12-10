using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class Episode_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;

        public Episode_controller()
        {
            this.conn = new DbConnect().GetConnection();
        }
        public void SaveTap(string episodeName, int movieID, string vidPath)
        {
            int row = 0;
            string sql = "INSERT INTO EPISODES (EPISODE_NAME, MOVIE_ID, VIDEO) VALUES (@episodeName, @movieId, @videoPath)";
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection()) 
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@episodeName", episodeName);
                        cmd.Parameters.AddWithValue("@movieId", movieID);
                        cmd.Parameters.AddWithValue("videoPath", vidPath);
                        row = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(episodeName + movieID + vidPath, "Lỗi: " + ex.Message);
            }
        }

        public List<Tap_model> GetEpisodesByMovieID(int movieID)
        {
            List<Tap_model> list = new List<Tap_model>();
            string sql = "SELECT EPISODE_NAME, MOVIE_ID, VIDEO FROM EPISODES WHERE MOVIE_ID = @movieID ORDER BY EPISODE_NAME";
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@movieID", movieID);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read()) // Đọc từng dòng trong kết quả trả về
                            {
                                string EpisodeName = reader.GetString(reader.GetOrdinal("EPISODE_NAME"));  // Gán giá trị cho tên tập
                                string VideoPath = reader.GetString(reader.GetOrdinal("VIDEO"));         // Gán giá trị cho đường dẫn video
                                Tap_model episode = new Tap_model(EpisodeName, movieID, VideoPath);
                                list.Add(episode); // Thêm tập vào danh sách
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu: " + ex.Message);
            }
            return list;
        }

        public bool checkTap(int movieId)
        {
            string sql = "SELECT * FROM EPISODES WHERE MOVIE_ID = @movieID";
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@movieID", movieId);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (SqlException ex) 
            {
                MessageBox.Show(ex.Message);
            } 
            return false;
        }
    }
}
