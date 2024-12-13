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
        private Genre_model genre;
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
    }
}
