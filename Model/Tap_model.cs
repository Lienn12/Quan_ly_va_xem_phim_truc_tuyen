using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.Model
{
    public class Tap_model
    {
        private int epID {  get; set; }
        public string epName { get; set; }
        private int movieId { get; set; }
        public string vidPathTap {  get; set; }

        public Tap_model()
        {
        }

        public Tap_model(string epName, int movieId, string vidPathTap)
        {
            this.epName = epName;
            this.movieId = movieId;
            this.vidPathTap = vidPathTap;
        }

        public Tap_model(int epID, string epName, int movieID, string vidPathTap)
        {
            this.epID = epID;
            this.epName = epName;
            this.movieId = movieID;
            this.vidPathTap = vidPathTap;
        }

        public override string ToString()
        {
            return $"Episode Name: {epName}, Movie ID: {movieId}, Video: {vidPathTap}";
        }
    }
}
