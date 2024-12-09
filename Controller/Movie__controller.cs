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
    public class Movie__controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        public Movie__controller()
        {
            this.conn = new DbConnect().GetConnection();
        }
        public List<Movie_model> GetMovies()
        {
            List<Movie_model> movieList = new List<Movie_model>();
            try
            {                
                conn.Open();
                string sql = "SELECT MOVIE_ID, TITLE, RELEASE_YEAR FROM MOVIES";
                using (cmd = new SqlCommand(sql, conn))
                {
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Movie_model movie = new Movie_model(reader);
                            movieList.Add(movie);
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return movieList;
        }


        public bool SaveInfo(string name, int year, string director, string cast, int genreID, int formatID, int countryID, int episode, string descrip, string imgPath, string vidPath)
        {
            int row = 0;
            string sql = @"INSERT INTO MOVIES (TITLE, RELEASE_YEAR, DIRECTOR, CAST, GENRE_ID, FORMAT_ID, COUNTRY_ID, TOTAL_EPISODES, DESCRIPTION, COVER_IMAGE, TRAILER)
                   VALUES (@Title, @ReleaseYear, @Director, @Cast, @GenreID, @FormatID, @CountryID, @Episode, @Description, @CoverImage, @Trailer)";
            try
            {
                conn.Open();
                using (cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Title", name);
                    cmd.Parameters.AddWithValue("@ReleaseYear", year);
                    cmd.Parameters.AddWithValue("@Director", director);
                    cmd.Parameters.AddWithValue("@Cast", cast);
                    cmd.Parameters.AddWithValue("@GenreID", genreID);
                    cmd.Parameters.AddWithValue("@FormatID", formatID);
                    cmd.Parameters.AddWithValue("@CountryID", countryID);
                    cmd.Parameters.AddWithValue("@Episode", episode);
                    cmd.Parameters.AddWithValue("@Description", descrip);
                    cmd.Parameters.AddWithValue("@CoverImage", imgPath);
                    cmd.Parameters.AddWithValue("@Trailer", vidPath);
                    row = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(name + year + director + cast + genreID + countryID + formatID + episode + descrip + imgPath + vidPath, "Lỗi: " + ex.Message);
            }
            return row > 0;
        }
    }
}
