using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quan_ly_thu_vien_phim.Model
{
    public class Genre_model
    {
        public int GenreID { get; set; }
        public string GenreName { get; set; }
        public Genre_model() { }
        public Genre_model(int genreID, string genreName)
        {
            GenreID = genreID;
            GenreName = genreName;
        }

        public override string ToString()
        {
            return GenreName;
        }
    }
}
