using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class DanhGia_model
    {
        public int ReviewId { get; set; }       // ID Đánh Giá
        public string Username { get; set; }   // Tên Tài Khoản
        public string MovieTitle { get; set; } // Tên Phim
        public int Rating { get; set; }        // Rating
        public string ReviewDate { get; set; } // Ngày Đánh Giá

        public DanhGia_model() { }

        public DanhGia_model( int reviewId, string username, string movieTitle, int rating, string reviewDate) 
        {
            ReviewId = reviewId;
            Username = username;
            MovieTitle = movieTitle;
            Rating = rating;
            ReviewDate = reviewDate;

        }
    }

}
