using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_useControl
{
    public partial class ItemReviewPhim  : UserControl
    {
        private Movie_model movieModel;
        public ItemReviewPhim(Movie_model movieModel)
        {
            this.movieModel = movieModel;
            InitializeComponent();
            SetProperties();
            LoadImage();
        }
        public Model.Movie_model GetData()
        {
            return movieModel;
        }
        private void SetProperties()
        {
            this.BackColor = Color.Transparent;
            lbTitle.Text = movieModel.Title;
            lbRating.Text = movieModel.Rating.ToString();
            pictureBox.Size = new Size(80, 90);
        }

        private void LoadImage()
        {
            if (!string.IsNullOrEmpty(movieModel.ImgPath))
            {
                try
                {
                    // Tải ảnh từ đường dẫn
                    Image image = Image.FromFile(movieModel.ImgPath);
                    // Thay đổi kích thước hình ảnh phù hợp với kích thước của lbImg
                    Image scaledImg = new Bitmap(image, pictureBox.Size);
                    pictureBox.Image = scaledImg;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    pictureBox.Text = "Error loading image";
                }
            }
            else
            {
                pictureBox.Text = "No Image";
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (Brush brush = new SolidBrush(Color.FromArgb(128, 255, 255, 255)))
            {
                e.Graphics.FillRectangle(brush, 0, 0, this.Width, this.Height);
            }
        }

        internal void DrawToBitmap(Graphics graphics, Rectangle rectangle)
        {
            throw new NotImplementedException();
        }
    }
}
