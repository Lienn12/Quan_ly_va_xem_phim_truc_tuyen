using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class Favourite_model
    {
        public int FavouriteId {  get; set; }
        public User_model User { get; set; }
        public Movie_model Movie { get; set; }
        public Favourite_model() { }
        public Favourite_model(int favouriteId, User_model user, Movie_model movie)
        {
            FavouriteId = favouriteId;
            User = user;
            Movie = movie;
        }
        public Favourite_model(SqlDataReader rs)
        {
            this.FavouriteId = rs.GetInt32(rs.GetOrdinal("favorite_Id"));
            int userId = rs.GetInt32(rs.GetOrdinal("User_ID"));
            this.User = new User_model();
            this.User.userId = userId;
            string title = rs.GetString(rs.GetOrdinal("Title"));
            int year = rs.GetInt32(rs.GetOrdinal("Release_year"));
            this.Movie = new Movie_model();
            this.Movie.Title = title;
            this.Movie.Year = year;
        }

    }
}
