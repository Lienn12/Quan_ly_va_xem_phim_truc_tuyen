using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class XemChiTiet : Form
    {
        private FormMain formMain;
        private string filePath, vidPath;
        private Movie_controller controller;
        public XemChiTiet(FormMain formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
        }

        private void XemChiTiet_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rc = this.ClientRectangle; // Kích thước của form

            // Tạo LinearGradientBrush từ #052659 đến #1CB5E0
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rc,
                Color.FromArgb(5, 38, 89),   // Màu bắt đầu (#052659)
                Color.FromArgb(28, 181, 224), // Màu kết thúc (#1CB5E0)
                LinearGradientMode.Vertical)) // Loang theo chiều dọc
            {
                g.FillRectangle(brush, rc); // Tô màu loang cho toàn bộ form
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String f = vidPath;
            PhatVideo playVideo = new PhatVideo();
            playVideo.setVideo(f);
            playVideo.Show();
        }

        private void LoadDanhSachTap()
        {
            pnlSoTap.Controls.Clear(); // Xóa các nút cũ nếu có

            // Danh sách các tập phim
            List<string> danhSachTap = new List<string> { "Tập 1", "Tập 2", "Tập 3", "Tập 4", "Tập 5", "Tập 6" };

            int y = 15;  // Vị trí bắt đầu của các nút

            foreach (var tap in danhSachTap)
            {
                Button btnCacTap = new Button
                {
                    Text = tap,
                    Size = new Size(95, 40),  // Kích thước nút
                    Location = new Point(10 + (110 * (pnlSoTap.Controls.Count % 6)), y), // Đặt vị trí của nút
                    Margin = new Padding(5),  // Khoảng cách giữa các nút
                    BackColor = Color.Gray,   // Màu nền nút
                    ForeColor = Color.White   // Màu chữ
                };

                // Thêm sự kiện Click cho mỗi nút
                btnCacTap.Click += (s, e) =>
                {
                    // Reset lại màu sắc của các nút
                    foreach (Button btn in pnlSoTap.Controls)
                    {
                        btn.BackColor = Color.Gray;
                    }

                    // Đổi màu cho nút được chọn
                    btnCacTap.BackColor = Color.Orange;
                    MessageBox.Show($"Đang phát: {tap}");
                };

                pnlSoTap.Controls.Add(btnCacTap);  // Thêm nút vào panel
            }
        }

        private void XemChiTiet_Load(object sender, EventArgs e)
        {
            LoadDanhSachTap();
        }

        private void btnTap_Click(object sender, EventArgs e)
        {
            pnlSoTap.Visible = !pnlSoTap.Visible;

            // Cập nhật nội dung của nút khi hiển thị/ẩn danh sách
            if (pnlSoTap.Visible)
            {
                btnTap.Text = "Ẩn danh sách";
            }
            else
            {
                btnTap.Text = "Xem tập";
            }
        }
        public void SetMovieDetails(Movie_model movie)
        {
            if (movie == null)
            {
                Console.WriteLine("Lỗi truy vấn");
                return;
            }
            lblNam.Text = movie.Year.ToString();
            lblTenPhim.Text = movie.Title;

            if (movie.Genre != null)
            {
                lblTheLoai.Text = movie.Genre.GenreName;
            }
            else
            {
                lblTheLoai.Text = "Không có thể loại";
            }

            lblMota.Text = movie.Description;
            lblDienVien.Text = movie.Cast;

            if (movie.Country != null)
            {
                lblQuocGia.Text = movie.Country.CountryName;
            }
            else
            {
                lblQuocGia.Text = "";
            }

            lblDaoDien.Text = movie.Director;
            if (movie.Format != null)
            {
                lblDinhDang.Text = movie.Format.FormatName;
            }
            else
            {
                lblDinhDang.Text = "";
            }

            filePath = movie.ImgPath;
            var img = Image.FromFile(filePath);
            var resizedImg = new Bitmap(img, pbImage.Width, pbImage.Height);
            pbImage.Image = resizedImg;
            vidPath = movie.VidPath;
        }
         public void showMovie(int movieId)
        {
            try
            {
                Movie_model movie = controller.GetMovieById(movieId);
                if (movie != null)
                {
                    SetMovieDetails(movie);
                }
                else
                {
                    Console.WriteLine("Không tìm thấy movie.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(movieId + $"Lỗi: {e.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
