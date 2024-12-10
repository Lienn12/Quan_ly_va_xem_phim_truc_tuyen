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
                conn.Open();
                string sql = "SELECT MOVIE_ID, TITLE, RELEASE_YEAR FROM MOVIES";
                using (cmd = new SqlCommand(sql, conn))
                {
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int movieId = reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                            string title = reader.GetString(reader.GetOrdinal("TITLE"));
                            int year = reader.GetInt32(reader.GetOrdinal("RELEASE_YEAR"));
                            Movie_model movie = new Movie_model(movieId,title,year);
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
        public bool UpdateMovie(Movie_model movie)
        {
            string query = @"UPDATE MOVIES SET TITLE = @Title, RELEASE_YEAR = @ReleaseYear, DIRECTOR = @Director, CAST = @Cast, GENRE_ID = @GenreId, FORMAT_ID = @FormatId, COUNTRY_ID = @CountryId, TOTAL_EPISODES = @Episodes, DESCRIPTION = @Description, COVER_IMAGE = @imgPath, TRAILER = @vidPath 
                         WHERE MOVIE_ID = @MovieID";
            try
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
                conn.Open();
                using (SqlTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using ( cmd = new SqlCommand(deleteFavorite, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@movie_id", movieId);
                            cmd.ExecuteNonQuery();
                        }
                        using ( cmd = new SqlCommand(deleteReviews, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@movie_id", movieId);
                            cmd.ExecuteNonQuery();
                        }
                        int rows;
                        using ( cmd = new SqlCommand(deleteMovies, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@movie_id", movieId);
                            rows = cmd.ExecuteNonQuery();
                        }
                        transaction.Commit(); // Commit transaction
                        return rows > 0;
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); // Rollback transaction in case of error
                        throw;
                    }
                }
            }
        }

        public List<Movie_model> GetImage()
        {
            List<Movie_model> dsMovie = new List<Movie_model>();
            string query = "SELECT MOVIE_ID, TITLE, RATING, COVER_IMAGE, DESCRIPTION FROM MOVIES";
            try
            {                
                conn.Open();
                using (cmd = new SqlCommand(query, conn))
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int movieId = reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                        string title = reader.GetString(reader.GetOrdinal("TITLE"));
                        float rating = reader.GetFloat(reader.GetOrdinal("RATING"));                        
                        string description = reader.GetString(reader.GetOrdinal("DESCRIPTION"));
                        string img = reader.GetString(reader.GetOrdinal("COVER_IMAGE"));
                        Movie_model movie = new Movie_model(movieId, title, description, rating, img);
                        dsMovie.Add(movie);
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return dsMovie;
        }
        public List<Movie_model> getPhimBo()
        {
            List<Movie_model> dsMovie = new List<Movie_model>();
            string query = @"SELECT MOVIE_ID, TITLE, RATING, COVER_IMAGE, DESCRIPTION FROM MOVIES JOIN FORMATS ON MOVIES.FORMAT_ID = FORMATS.FORMAT_ID
                            WHERE FORMAT_NAME = N'Phim bộ'";
            try
            {
                conn.Open();
                using (cmd = new SqlCommand(query, conn))
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int movieId = reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                        string title = reader.GetString(reader.GetOrdinal("TITLE"));
                        float rating = reader.GetFloat(reader.GetOrdinal("RATING"));
                        string description = reader.GetString(reader.GetOrdinal("DESCRIPTION"));
                        string img = reader.GetString(reader.GetOrdinal("COVER_IMAGE"));
                        Movie_model movie = new Movie_model(movieId, title, description, rating, img);
                        dsMovie.Add(movie);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return dsMovie;
        }
        public List<Movie_model> getPhimLe()
        {
            List<Movie_model> dsMovie = new List<Movie_model>();
            string query = @"SELECT MOVIE_ID, TITLE, RATING, COVER_IMAGE, DESCRIPTION FROM MOVIES JOIN FORMATS ON MOVIES.FORMAT_ID = FORMATS.FORMAT_ID
                            WHERE FORMAT_NAME = N'Phim lẻ'";
            try
            {
                conn.Open();
                using (cmd = new SqlCommand(query, conn))
                using (reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int movieId = reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                        string title = reader.GetString(reader.GetOrdinal("TITLE"));
                        float rating = reader.GetFloat(reader.GetOrdinal("RATING"));
                        string description = reader.GetString(reader.GetOrdinal("DESCRIPTION"));
                        string img = reader.GetString(reader.GetOrdinal("COVER_IMAGE"));
                        Movie_model movie = new Movie_model(movieId, title, description, rating, img);
                        dsMovie.Add(movie);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            return dsMovie;
        }
        public Movie_model GetMovieById(int movieId)
        {
            string query = @"SELECT MOVIE_ID, TITLE, RELEASE_YEAR, GENRE_NAME, COUNTRY_NAME, FORMAT_NAME, DIRECTOR, CAST, DESCRIPTION, TOTAL_EPISODES, COVER_IMAGE, TRAILER FROM MOVIES
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
                                string name = reader["TITLE"].ToString();
                                int year = Convert.ToInt32(reader["RELEASE_YEAR"]);
                                string genre = reader["GENRE_NAME"].ToString();
                                string format = reader["FORMAT_NAME"].ToString();
                                string country = reader["COUNTRY_NAME"].ToString();
                                string director = reader["DIRECTOR"].ToString();
                                string cast = reader["CAST"].ToString();
                                string description = reader["DESCRIPTION"].ToString();
                                int episode = Convert.ToInt32(reader["TOTAL_EPISODES"]);
                                string img = reader["COVER_IMAGE"].ToString();  
                                string vidPath = reader["TRAILER"].ToString();

                                Genre_model genreModel = string.IsNullOrEmpty(genre) ? null : new Genre_model(0, genre);
                                Format_model formatModel = string.IsNullOrEmpty(format) ? null : new Format_model(0, format);
                                Country_model countryModel = string.IsNullOrEmpty(country) ? null : new Country_model(0, country);

                                // Trả về đối tượng Movie_model
                                return new Movie_model(
                                    movieId, name, year, director, cast,
                                    genreModel, formatModel, countryModel, description, episode, img, vidPath
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
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("GetMovieById: MovieModel is null");
            return null;
        }

    }
}
