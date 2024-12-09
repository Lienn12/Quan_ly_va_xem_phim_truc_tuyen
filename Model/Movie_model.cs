using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class Movie_model
    {
        public int Id { get; set; } 
        public string Title { get; set; } 
        public int Year { get; set; }
        public string Director { get; set; } 
        public string Cast { get; set; }
        public string Type { get; set; } 
        public string Format { get; set; } 
        public string Country { get; set; } 
        public string Description { get; set; }
        public int Episode { get; set; } 
        public string ImgPath { get; set; }
        public string VidPath { get; set; } 
        
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

     
        public override string ToString()
        {
            return $"ID: {Id}, Title: {Title}, Year: {Year}, Director: {Director}, Cast: {Cast}, Type: {Type}, Format: {Format}, Country: {Country}, Description: {Description}, Episode: {Episode},ImgPath: {ImgPath}, VidPath: {VidPath}";
        }
    }
}
