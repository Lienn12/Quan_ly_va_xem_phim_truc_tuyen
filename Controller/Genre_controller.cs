﻿using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Controller
{
    public class Genre_controller
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader reader = null;
        //private Countries country;
        //public Genre_controller()
        //{
        //    conn = new DbConnect().GetConnection();
        //}
        //public List<Genres> GetGenres()
        //{
        //    List<Genres> GenreList = new List<Genres>();
        //    try
        //    {
        //        string sql = "SELECT * FROM GENRES";
        //        cmd = new SqlCommand(sql, conn);
        //        conn.Open();
        //        reader = cmd.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            int genreId = reader.GetInt32(reader.GetOrdinal("GENRE_ID"));
        //            string genreName = reader.GetString(reader.GetOrdinal("GENRE_NAME"));
        //            // Tạo đối tượng Formats và thêm vào danh sách
        //            Genres genre = new Genres(genreId, genreName);
        //            GenreList.Add(genre);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Lỗi: " + ex.Message);
        //    }
        //    finally
        //    {
        //        if (reader != null) reader.Close();
        //        if (conn != null) conn.Close();
        //    }
        //    return GenreList;
        //}
    }
}
