using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class Favourite_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        public Favourite_controller()
        {
            conn = new DbConnect().GetConnection();
        }
        public List<Favourite_model> GetFavorite(int userId)
        {
            List<Favourite_model> dsFavorite = new List<Favourite_model>();
            string sql = "SELECT FAVORITE_ID, USER_ID, TITLE, RELEASE_YEAR FROM FAVORITEs, MOVIES WHERE MOVIES.MOVIE_ID = FAVORITEs.MOVIE_ID AND USER_ID = @userId";

            using (conn)
            {
                using (cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    conn.Open();
                    using (reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Favourite_model favorite = new Favourite_model(reader);
                            dsFavorite.Add(favorite);
                        }
                    }
                }

            }
            
            return dsFavorite;
        }
        public int GetMovieId(int favoriteID)
        {
            string sql = "SELECT MOVIE_ID FROM FAVORITES WHERE FAVORITE_ID = @favoriteID";
            using (conn)
            {
                using (cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@favoriteID", favoriteID);
                    conn.Open();
                    using (reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(reader.GetOrdinal("MOVIE_ID"));
                        }
                        else
                        {
                            throw new Exception($"Không tìm thấy MOVIE_ID cho FAVORITE_ID = {favoriteID}");
                        }
                    }
                }
            }
            
        }

        public int GetUserId(int favoriteID)
        {
            string sql = "SELECT USER_ID FROM FAVORITES WHERE FAVORITE_ID = @favoriteID";
            using(conn)
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@favoriteID", favoriteID);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader.GetInt32(reader.GetOrdinal("USER_ID"));
                        }
                        else
                        {
                            throw new Exception($"Không tìm thấy USER_ID cho FAVORITE_ID = {favoriteID}");
                        }
                    }
                }
            }            
        }

        public bool InsertFavorite(int movieId, int userId)
        {
            string sql = "INSERT INTO FAVORITES (MOVIE_ID, USER_ID) VALUES (@movieId, @userId)";
            using (conn)
            {
                using (cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@movieId", movieId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    conn.Open();
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
        }

        public bool CheckMovie(int movieId, int userId)
        {
            string sql = "SELECT FAVORITE_ID FROM FAVORITES WHERE USER_ID = @userId AND MOVIE_ID = @movieId";
            using (conn)
            {
                using (cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@movieId", movieId);
                    conn.Open();
                    using (reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

        public bool DeleteFavorite(int favoriteID)
        {
            string sql = "DELETE FROM FAVORITES WHERE FAVORITE_ID = @favoriteID";
            using (conn)
            {
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@favoriteID", favoriteID);
                    conn.Open();
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
        }
    }
}
