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
    public class Movie_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        public Movie_controller()
        {
            this.conn = new DbConnect().GetConnection();
        }
        public List<Movie_model> GetMovies()
        {
            List<Movie_model> movieList = new List<Movie_model>();
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    string sql = "SELECT MOVIE_ID, TITLE, RELEASE_YEAR FROM MOVIES";
                    using (cmd = new SqlCommand(sql, conn))
                    {
                        using (reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int movieId = reader.IsDBNull(reader.GetOrdinal("MOVIE_ID")) ? 0 : reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                                string title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? string.Empty : reader.GetString(reader.GetOrdinal("TITLE"));
                                int year = reader.IsDBNull(reader.GetOrdinal("RELEASE_YEAR")) ? 0 : reader.GetInt32(reader.GetOrdinal("RELEASE_YEAR"));

                                Movie_model movie = new Movie_model(movieId, title, year);
                                movieList.Add(movie);
                            }
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
                using (conn = new DbConnect().GetConnection())
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(name + year + director + cast + genreID + countryID + formatID + episode + descrip + imgPath + vidPath, "Lỗi: " + ex.Message);
            }
            return row > 0;
        }
        public bool UpdateMovie(Movie_model movie)
        {
            string query = @"UPDATE MOVIES SET TITLE = @Title, RELEASE_YEAR = @ReleaseYear, DIRECTOR = @Director, CAST = @Cast, GENRE_ID = @GenreId, FORMAT_ID = @FormatId, COUNTRY_ID = @CountryId, TOTAL_EPISODES = @Episodes, DESCRIPTION = @Description, COVER_IMAGE = @imgPath, TRAILER = @vidPath 
                         WHERE MOVIE_ID = @MovieID";
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", movie.Title);
                        cmd.Parameters.AddWithValue("@ReleaseYear", movie.Year);
                        cmd.Parameters.AddWithValue("@Director", movie.Director);
                        cmd.Parameters.AddWithValue("@Cast", movie.Cast);
                        cmd.Parameters.AddWithValue("@GenreId", movie.Genre.GenreID);
                        cmd.Parameters.AddWithValue("@FormatId", movie.Format.FormatID);
                        cmd.Parameters.AddWithValue("@CountryId", movie.Country.CountryId);
                        cmd.Parameters.AddWithValue("@Episodes", movie.Episode);
                        cmd.Parameters.AddWithValue("@Description", movie.Description ?? string.Empty);
                        cmd.Parameters.AddWithValue("@imgPath", movie.ImgPath ?? string.Empty);
                        cmd.Parameters.AddWithValue("@vidPath", movie.VidPath ?? string.Empty);
                        cmd.Parameters.AddWithValue("@MovieID", movie.MovieId);

                        int rowsUpdated = cmd.ExecuteNonQuery();
                        return rowsUpdated > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteFilm(int movieId)
        {
            string deleteFavorite = "DELETE FROM FAVORITES WHERE MOVIE_ID = @movie_id";
            string deleteReviews = "DELETE FROM Reviews WHERE MOVIE_ID = @movie_id";
            string deleteMovies = "DELETE FROM Movies WHERE MOVIE_ID = @movie_id";
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (cmd = new SqlCommand(deleteFavorite, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@movie_id", movieId);
                                cmd.ExecuteNonQuery();
                            }
                            using (cmd = new SqlCommand(deleteReviews, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@movie_id", movieId);
                                cmd.ExecuteNonQuery();
                            }
                            int rows;
                            using (cmd = new SqlCommand(deleteMovies, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@movie_id", movieId);
                                rows = cmd.ExecuteNonQuery();
                            }
                            transaction.Commit(); 
                            return rows > 0;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback(); 
                            throw;
                        }
                    }
                }
            }
        }

        public List<Movie_model> GetDeXuat()
        {
            List<Movie_model> dsMovie = new List<Movie_model>();
            string query = "SELECT MOVIE_ID, TITLE, RATING, COVER_IMAGE, DESCRIPTION FROM MOVIES";
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int movieId = reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                            string title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? string.Empty : reader.GetString(reader.GetOrdinal("TITLE"));
                            string description = reader.IsDBNull(reader.GetOrdinal("DESCRIPTION")) ? string.Empty : reader.GetString(reader.GetOrdinal("DESCRIPTION"));
                            float rating = reader.IsDBNull(reader.GetOrdinal("RATING")) ? 0.0f : Convert.ToSingle(reader.GetDouble(reader.GetOrdinal("RATING")));
                            string imgPath = reader.IsDBNull(reader.GetOrdinal("COVER_IMAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("COVER_IMAGE"));
                            Movie_model movie = new Movie_model(movieId, title, description, rating, imgPath);
                            dsMovie.Add(movie);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return dsMovie;
        }


        public List<Movie_model> GetPhimBo()
        {
            List<Movie_model> dsMovie = new List<Movie_model>();
            string query = @"SELECT MOVIE_ID, TITLE, RATING, COVER_IMAGE, DESCRIPTION FROM MOVIES JOIN FORMATS ON MOVIES.FORMAT_ID = FORMATS.FORMAT_ID
                            WHERE FORMAT_NAME = N'Phim bộ'";
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(query, conn))
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int movieId = reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                            string title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? string.Empty : reader.GetString(reader.GetOrdinal("TITLE"));
                            string description = reader.IsDBNull(reader.GetOrdinal("DESCRIPTION")) ? string.Empty : reader.GetString(reader.GetOrdinal("DESCRIPTION"));
                            float rating = reader.IsDBNull(reader.GetOrdinal("RATING")) ? 0.0f : Convert.ToSingle(reader.GetDouble(reader.GetOrdinal("RATING")));
                            string imgPath = reader.IsDBNull(reader.GetOrdinal("COVER_IMAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("COVER_IMAGE"));
                            Movie_model movie = new Movie_model(movieId, title, description, rating, imgPath);
                            dsMovie.Add(movie);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            return dsMovie;
        }
        public List<Movie_model> GetPhimLe()
        {
            List<Movie_model> dsMovie = new List<Movie_model>();
            string query = @"SELECT MOVIE_ID, TITLE, RATING, COVER_IMAGE, DESCRIPTION FROM MOVIES JOIN FORMATS ON MOVIES.FORMAT_ID = FORMATS.FORMAT_ID
                            WHERE FORMAT_NAME = N'Phim lẻ'";
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (cmd = new SqlCommand(query, conn))
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int movieId = reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                            string title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? string.Empty : reader.GetString(reader.GetOrdinal("TITLE"));
                            string description = reader.IsDBNull(reader.GetOrdinal("DESCRIPTION")) ? string.Empty : reader.GetString(reader.GetOrdinal("DESCRIPTION"));
                            float rating = reader.IsDBNull(reader.GetOrdinal("RATING")) ? 0.0f : Convert.ToSingle(reader.GetDouble(reader.GetOrdinal("RATING")));
                            string imgPath = reader.IsDBNull(reader.GetOrdinal("COVER_IMAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("COVER_IMAGE"));
                            Movie_model movie = new Movie_model(movieId, title, description, rating, imgPath);
                            dsMovie.Add(movie);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            return dsMovie;
        }
        public Movie_model GetMovieById(int movieId)
        {
            string query = @"SELECT MOVIE_ID, TITLE, RELEASE_YEAR,RATING, GENRE_NAME, COUNTRY_NAME, FORMAT_NAME, DIRECTOR, CAST, DESCRIPTION, TOTAL_EPISODES, COVER_IMAGE, TRAILER FROM MOVIES
                            INNER JOIN COUNTRIES ON MOVIES.COUNTRY_ID = COUNTRIES.COUNTRY_ID
                            INNER JOIN GENRES ON MOVIES.GENRE_ID = GENRES.GENRE_ID
                            INNER JOIN FORMATS ON MOVIES.FORMAT_ID = FORMATS.FORMAT_ID
                            WHERE MOVIE_ID = @movieId;";
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection()) 
                {
                    if (conn == null)
                    {
                        Console.WriteLine("Không thể kết nối đến cơ sở dữ liệu.");
                        return null;
                    }
                    conn.Open();
                    using (cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@movieId", movieId);
                        using (reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string name = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? string.Empty : reader["TITLE"].ToString();
                                int year = reader.IsDBNull(reader.GetOrdinal("RELEASE_YEAR")) ? 0 : Convert.ToInt32(reader["RELEASE_YEAR"]);
                                float rating = reader.IsDBNull(reader.GetOrdinal("RATING")) ? 0 : Convert.ToSingle(reader["RATING"]);
                                string genre = reader.IsDBNull(reader.GetOrdinal("GENRE_NAME")) ? string.Empty : reader["GENRE_NAME"].ToString();
                                string format = reader.IsDBNull(reader.GetOrdinal("FORMAT_NAME")) ? string.Empty : reader["FORMAT_NAME"].ToString();
                                string country = reader.IsDBNull(reader.GetOrdinal("COUNTRY_NAME")) ? string.Empty : reader["COUNTRY_NAME"].ToString();
                                string director = reader.IsDBNull(reader.GetOrdinal("DIRECTOR")) ? string.Empty : reader["DIRECTOR"].ToString();
                                string cast = reader.IsDBNull(reader.GetOrdinal("CAST")) ? string.Empty : reader["CAST"].ToString();
                                string description = reader.IsDBNull(reader.GetOrdinal("DESCRIPTION")) ? string.Empty : reader["DESCRIPTION"].ToString();
                                int episode = reader.IsDBNull(reader.GetOrdinal("TOTAL_EPISODES")) ? 0 : Convert.ToInt32(reader["TOTAL_EPISODES"]);
                                string img = reader.IsDBNull(reader.GetOrdinal("COVER_IMAGE")) ? string.Empty : reader["COVER_IMAGE"].ToString();
                                string vidPath = reader.IsDBNull(reader.GetOrdinal("TRAILER")) ? string.Empty : reader["TRAILER"].ToString();

                                return new Movie_model(
                                    movieId, name,rating, year, director, cast,
                                    new Genre_model(0, genre), new Format_model(0, format), new Country_model(0, country), description, episode, img, vidPath
                                );
                            }
                            else
                            {
                                Console.WriteLine($"Movie not found for movieID: {movieId}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            Console.WriteLine("GetMovieById: MovieModel is null");
            return null;
        }

        public List<Movie_model> GetReview()
        {
            List<Movie_model> dsMovie = new List<Movie_model>();
            string sql = "SELECT TOP 5 MOVIE_ID, TITLE, RATING, COVER_IMAGE FROM MOVIES ORDER BY RATING DESC";
            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int movieId = reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                                string title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? string.Empty : reader.GetString(reader.GetOrdinal("TITLE"));
                                float rating = reader.IsDBNull(reader.GetOrdinal("RATING")) ? 0.0f : Convert.ToSingle(reader.GetDouble(reader.GetOrdinal("RATING")));
                                string imgPath = reader.IsDBNull(reader.GetOrdinal("COVER_IMAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("COVER_IMAGE"));

                                Movie_model movie = new Movie_model(movieId, title, rating, imgPath);
                                dsMovie.Add(movie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing query GetReview(): " + ex.Message);
            }
            return dsMovie;
        }
        public List<Movie_model> GetMoviesByGenreID(int genreId)
        {
            string sql = @"
            SELECT MOVIE_ID, TITLE, RATING, COVER_IMAGE
            FROM MOVIES, GENRES 
            WHERE MOVIES.GENRE_ID = GENRES.GENRE_ID 
            AND GENRES.GENRE_ID = @genreId";

            List<Movie_model> dsMovie = new List<Movie_model>();

            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@genreId", genreId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int movieId = reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                                string title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? string.Empty : reader.GetString(reader.GetOrdinal("TITLE"));
                                float rating = reader.IsDBNull(reader.GetOrdinal("RATING")) ? 0.0f : Convert.ToSingle(reader.GetDouble(reader.GetOrdinal("RATING")));
                                string imgPath = reader.IsDBNull(reader.GetOrdinal("COVER_IMAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("COVER_IMAGE"));

                                Movie_model movie = new Movie_model(movieId, title, rating, imgPath);
                                dsMovie.Add(movie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving movies by genre: " + ex.Message);
            }

            return dsMovie;
        }

        public List<Movie_model> GetMoviesByCountryID(int countryId)
        {
            string sql = @"
            SELECT MOVIE_ID, TITLE, RATING, COVER_IMAGE
            FROM MOVIES,COUNTRIES
            WHERE MOVIES.COUNTRY_ID = COUNTRIES.COUNTRY_ID
            AND COUNTRIES.COUNTRY_ID = @countryId";


            List<Movie_model> dsMovie = new List<Movie_model>();

            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@countryId", countryId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int movieId = reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                                string title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? string.Empty : reader.GetString(reader.GetOrdinal("TITLE"));
                                float rating = reader.IsDBNull(reader.GetOrdinal("RATING")) ? 0.0f : Convert.ToSingle(reader.GetDouble(reader.GetOrdinal("RATING")));
                                string imgPath = reader.IsDBNull(reader.GetOrdinal("COVER_IMAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("COVER_IMAGE"));

                                Movie_model movie = new Movie_model(movieId, title, rating, imgPath);
                                dsMovie.Add(movie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error retrieving movies by country: " + ex.Message);
            }

            return dsMovie;
        }
        public List<Movie_model> SearchMovies(int genre, int country, int format, int sort)
        {
            List<Movie_model> dsMovie = new List<Movie_model>();
            StringBuilder sql = new StringBuilder(@"
            SELECT MOVIE_ID, TITLE, RELEASE_YEAR, RATING, COVER_IMAGE, DESCRIPTION
            FROM MOVIES, COUNTRIES, GENRES, FORMATS
            WHERE MOVIES.COUNTRY_ID = COUNTRIES.COUNTRY_ID
            AND MOVIES.GENRE_ID = GENRES.GENRE_ID
            AND MOVIES.FORMAT_ID = FORMATS.FORMAT_ID
            AND (GENRES.GENRE_ID = @genre OR @genre = 1)
            AND (COUNTRIES.COUNTRY_ID = @country OR @country = 1)
            AND (FORMATS.FORMAT_ID = @format OR @format = 1)");

            if (sort == 1)
            {
                sql.Append(" ORDER BY TITLE ASC");
            }
            else if (sort == 2)
            {
                sql.Append(" ORDER BY RELEASE_YEAR DESC");
            }
            else if (sort == 3)
            {
                sql.Append(" ORDER BY RATING DESC");
            }

            try
            {
                using (SqlConnection conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql.ToString(), conn))
                    {
                        cmd.Parameters.AddWithValue("@genre", genre);
                        cmd.Parameters.AddWithValue("@country", country);
                        cmd.Parameters.AddWithValue("@format", format);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int movieId = reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                                string title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? string.Empty : reader.GetString(reader.GetOrdinal("TITLE"));
                                float rating = reader.IsDBNull(reader.GetOrdinal("RATING")) ? 0.0f : Convert.ToSingle(reader["RATING"]); // Sử dụng Convert.ToSingle để đảm bảo an toàn kiểu
                                string img = reader.IsDBNull(reader.GetOrdinal("COVER_IMAGE")) ? string.Empty : reader.GetString(reader.GetOrdinal("COVER_IMAGE"));

                                Movie_model movie = new Movie_model(movieId, title, rating, img);
                                dsMovie.Add(movie);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error executing query: " + ex.Message);
            }

            return dsMovie;
        }
    }

}
