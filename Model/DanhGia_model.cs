using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class DanhGia_model
    {
        public int ReviewId { get; set; }       // ID Đánh Giá
        public User_model User { get; set; }   // Tên Tài Khoản
        public Movie_model Movie { get; set; } // Tên Phim
        public int Rating { get; set; }        // Rating
        public string Comment {  get; set; }
        public DateTime ReviewDate { get; set; } // Ngày Đánh Giá
        public string Reply {  get; set; }
        public DanhGia_model() { }

        public DanhGia_model( int reviewId, User_model user, Movie_model movie, int rating, DateTime reviewDate) 
        {
            ReviewId = reviewId;
            User = user;
            Movie = movie;
            Rating = rating;
            ReviewDate = reviewDate;

        }
        public DanhGia_model(SqlDataReader reader)
        {
            this.ReviewId = reader.IsDBNull(reader.GetOrdinal("review_ID")) ? 0 : reader.GetInt32(reader.GetOrdinal("review_ID"));
            string title = reader.IsDBNull(reader.GetOrdinal("TITLE")) ? string.Empty : reader.GetString(reader.GetOrdinal("TITLE"));
            this.Movie = new Movie_model();
            this.Movie.Title = title;
            string username = reader.IsDBNull(reader.GetOrdinal("USERNAME")) ? string.Empty : reader.GetString(reader.GetOrdinal("USERNAME"));
            this.User = new User_model();
            this.User.username = username;
            this.ReviewDate = reader.IsDBNull(reader.GetOrdinal("review_Date")) ? DateTime.MinValue : reader.GetDateTime(reader.GetOrdinal("review_Date"));
            this.Rating = reader.IsDBNull(reader.GetOrdinal("Rating")) ? 0 : reader.GetInt32(reader.GetOrdinal("Rating"));
        }

        public DanhGia_model(int reviewID, string username, DateTime reviewDate, int rating, string comment, string img, string title, int releaseYear, string reply)
        {
            User = new User_model();
            User.username = username;
            Movie = new Movie_model();
            Movie.Title = title;
            Movie.Year = releaseYear;
            Movie.ImgPath = img;
            Comment = comment;
            ReviewId = reviewID;
            ReviewDate = reviewDate;
            Rating = rating;
            Reply = reply;
        }

        public DanhGia_model(string username, int rating, string comment, int movieId,string reply)
        {
            this.User = new User_model(username);  
            this.Movie = new Movie_model(movieId); 
            this.Comment = comment;
            this.Rating = rating;
            this.Reply = reply;
        }
    }

}
