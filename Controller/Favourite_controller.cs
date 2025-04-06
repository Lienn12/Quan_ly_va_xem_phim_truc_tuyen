﻿using Quan_ly_thu_vien_phim.Model;
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
            string sql = "SELECT FAVORITE_ID, USER_ID, TITLE, RELEASE_YEAR FROM FAVORITES, MOVIES WHERE MOVIES.MOVIE_ID = FAVORITEs.MOVIE_ID AND USER_ID = @userId";

            using (conn=new DbConnect().GetConnection())
            {   
                conn.Open();
                using (cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);                   
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
            using (conn = new DbConnect().GetConnection())
            { 
                conn.Open();
                using (cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@favoriteID", favoriteID);
                   
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
            using(conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@favoriteID", favoriteID);
                    
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
            using (conn = new DbConnect().GetConnection())
            { 
                conn.Open();
                using (cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@movieId", movieId);
                    cmd.Parameters.AddWithValue("@userId", userId);
                   
                    int row = cmd.ExecuteNonQuery();
                    return row > 0;
                }
            }
        }

        public bool CheckMovie(int movieId, int userId)
        {
            string sql = "SELECT FAVORITE_ID FROM FAVORITES WHERE USER_ID = @userId AND MOVIE_ID = @movieId";
            using (conn = new DbConnect().GetConnection())
            {
                conn.Open();
                using (cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@movieId", movieId);
                    
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

            try
            {
                using (var conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@favoriteID", SqlDbType.Int).Value = favoriteID;
                        int row = cmd.ExecuteNonQuery();
                        return row > 0; 
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi khi xóa yêu thích: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi: {ex.Message}");
                return false;
            }
        }
        public bool RemoveMovie(int movieId, int userId)
        {
            string sql = "DELETE FROM FAVORITES WHERE MOVIE_ID = @movieId AND USER_ID = @userId";

            try
            {
                using (var conn = new DbConnect().GetConnection())
                {
                    conn.Open();
                    using (var cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@movieId", movieId);
                        cmd.Parameters.AddWithValue("@userId", userId);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // trả về true nếu có dòng bị xóa
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("SQL Error: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }


    }
}
