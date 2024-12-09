using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class Movie_model
    {
        public int MovieId { get; set; } 
        public string Title { get; set; } 
        public int Year { get; set; } 
        public string Director { get; set; } 
        public string Cast { get; set; } 
        public Genre_model Genre { get; set; } 
        public Format_model Format { get; set; } 
        public Country_model Country { get; set; } 
        public string Description { get; set; }
        public int Episode { get; set; } 
        public string ImgPath { get; set; } 
        public string VidPath { get; set; } 
        // Constructor mặc định
        public Movie_model()
        {
        }

        public Movie_model(int movieId, string title, int year, string director, string cast, Genre_model genre, Format_model format, Country_model country, string description, int episode, string img, string vidPath)
        {
            this.MovieId = movieId;
            this.Title = title;
            this.Year = year;
            this.Director = director;
            this.Cast = cast;
            this.Genre = genre;
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
            return $"ID: {MovieId}, Title: {Title}, Year: {Year}, Director: {Director}, Cast: {Cast}, Type: {Genre}, Format: {Format}, Country: {Country}, Description: {Description}, Episode: {Episode},ImgPath: {ImgPath}, VidPath: {VidPath}";
        }
    }
}
