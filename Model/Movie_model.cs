using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class Movie_model
    {
        public int Id { get; set; } // ID duy nhất của phim
        public string Title { get; set; } // Tiêu đề phim
        public int Year { get; set; } // Năm sản xuất
        public string Director { get; set; } // Đạo diễn
        public string Cast { get; set; } // Dàn diễn viên (có thể là một chuỗi phân cách bởi dấu phẩy)
        public string Type { get; set; } // Thể loại phim (vd: Action, Drama, ...)
        public string Format { get; set; } // Định dạng phim (vd: HD, 4K, ...)
        public string Country { get; set; } // Quốc gia sản xuất
        public string Description { get; set; } // Mô tả phim
        public int Episode { get; set; } // Số tập phim (đối với series, nếu không thì mặc định là 1)
        public string ImgPath { get; set; } // Ảnh đại diện lưu dưới dạng mảng byte
        public string VidPath { get; set; } // Đường dẫn video

        // Constructor mặc định
        public Movie_model()
        {
        }

        public Movie_model(int id, string title, int year, string director, string cast, string type, string format, string country, string description, int episode, string img, string vidPath)
        {
            this.Id = id;
            this.Title = title;
            this.Year = year;
            this.Director = director;
            this.Cast = cast;
            this.Type = type;
            this.Format = format;
            this.Country = country;
            this.Description = description;
            this.Episode = episode;
            this.ImgPath = img;
            this.VidPath = vidPath;
        }

        // Override phương thức ToString() để dễ dàng hiển thị thông tin phim (không hiển thị ảnh vì là nhị phân)
        public override string ToString()
        {
            return $"ID: {Id}, Title: {Title}, Year: {Year}, Director: {Director}, Cast: {Cast}, Type: {Type}, Format: {Format}, Country: {Country}, Description: {Description}, Episode: {Episode},ImgPath: {ImgPath}, VidPath: {VidPath}";
        }
    }
}
