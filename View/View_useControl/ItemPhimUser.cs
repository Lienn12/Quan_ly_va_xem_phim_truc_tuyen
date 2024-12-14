using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Container;
using Quan_ly_thu_vien_phim.View.View_useControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Quan_ly_thu_vien_phim.View.View_Main
{
    public partial class ItemPhimUser  : UserControl
    {
        private Movie_model movieModel;
        private User_model userModel;
        private FormMainUser formMainUser;
        private Mota mota;
        private Timer timer;
        private Timer timerStopTimer;
        private int y = 120;
        private int speed = 20;
        private bool showing = false;
        private Image loadedImage;
        public ItemPhimUser(Movie_model movieModel, FormMainUser formMainUser, User_model userModel)
        {   
            this.userModel = userModel;
            this.movieModel = movieModel;
            this.formMainUser = formMainUser;
            InitializeComponent();
            Init();
            mota = new Mota(movieModel.Title, movieModel.Rating)
            {
                Location = new Point(0, y),
                Size = new Size(180, 180)
            };
            this.Controls.Add(mota);
            this.Click += (s, e) =>
            {
                showChiTietPhim(s);
            };
           
        }
        private void showChiTietPhim(object sender)
        {
            if (movieModel == null)
            {
                MessageBox.Show("Dữ liệu phim chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (formMainUser == null)
            {
                MessageBox.Show("Form chính chưa được khởi tạo!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            formMainUser.ShowMovieDetail(movieModel, sender);
            XemChiTietUser chiTietUser = formMainUser.GetXemChiTiet();
            chiTietUser.setShowBtnBackFavourite(true);
        }
        public void Init()
        {
            this.BackColor = Color.Transparent;
            this.Size = new Size(170, 210);
            this.MaximumSize = new Size(170, 210);
            this.MinimumSize = new Size(170, 210);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g2 = e.Graphics;
            g2.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            var gp = new System.Drawing.Drawing2D.LinearGradientBrush(
                new Point(0, this.Height),
                new Point(0, this.Height - 50),
                Color.FromArgb(0, 0, 0, 128),
                Color.FromArgb(0, 0, 0, 0)
            );
            g2.FillRectangle(gp, 0, 0, this.Width, this.Height);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            if (loadedImage == null && !string.IsNullOrEmpty(movieModel.ImgPath))
            {
                try
                {
                    loadedImage = Image.FromFile(movieModel.ImgPath); // Tải ảnh chỉ một lần
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading image: " + ex.Message);
                }
            }

            if (loadedImage != null)
            {
                var size = GetAutoSize(loadedImage);
                e.Graphics.DrawImage(loadedImage, size.X, size.Y, size.Width, size.Height);
            }
        }

        private Rectangle GetAutoSize(Image image)
        {
            int w = this.Width;
            int h = this.Height;
            int iw = image.Width;
            int ih = image.Height;

            double xScale = (double)w / iw;
            double yScale = (double)h / ih;
            double scale = Math.Max(xScale, yScale); // Đảm bảo tỷ lệ không bị biến dạng

            int width = (int)(scale * iw);
            int height = (int)(scale * ih);
            int x = (w - width) / 2;
            int y = (h - height) / 2;

            return new Rectangle(new Point(x, y), new Size(width, height));
        }

    }
}

