using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.Properties;
using Quan_ly_thu_vien_phim.View.View_Main;
using Quan_ly_thu_vien_phim.View.View_useControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class XemChiTietUser : Form
    {
        private FormMainUser formMainUser;
        private FormMain formMain;
        private string filePath, vidPath;
        private Movie_model movie;
        private User_model user;
        private Movie_controller controller = new Movie_controller();
        private Episode_controller episode_Controller = new Episode_controller();
        private DanhGia_controller danhgiaController = new DanhGia_controller();
        private Favourite_controller favouriteController = new Favourite_controller();
        private int idPhim;
        private int soStar = 0;
        public XemChiTietUser(FormMainUser formMainUser)
        {
            InitializeComponent();
            pnlSoTap.BackColor = Color.FromArgb(128, 255, 255, 255);
            this.formMainUser = formMainUser;
            pnlDanhGia.Visible = false;
            formMain = new FormMain();
            setShowBtnBackFavourite(false);
            setShowBtnBackTrangChu(false);
        }

        private void XemChiTietUser_Paint(object sender, PaintEventArgs e)
        {
                Graphics g = e.Graphics;
                Rectangle rc = this.ClientRectangle;

                using (LinearGradientBrush brush = new LinearGradientBrush(
                    rc,
                    Color.FromArgb(5, 38, 89),   
                    Color.FromArgb(28, 181, 224), 
                    LinearGradientMode.Vertical)) 
                {
                    g.FillRectangle(brush, rc); 
                }
        }
        private void btnTrailer_Click(object sender, EventArgs e)
        {
            String f = vidPath;
            PhatVideo playVideo = new PhatVideo();
            playVideo.setVideo(f);
            playVideo.Show();
        }
        private void LoadDanhSachTap()
        {
            pnlSoTap.Controls.Clear(); 
            List<Tap_model> danhSachTap = episode_Controller.GetEpisodesByMovieID(idPhim);
            int y = 15;  
            foreach (var tap in danhSachTap)
            {
                Button btnCacTap = new Button
                {
                    Text = tap.epName,
                    Size = new Size(95, 40), 
                    Location = new Point(10 + (110 * (pnlSoTap.Controls.Count % 6)), y), 
                    Margin = new Padding(5),  
                    BackColor = Color.Gray,  
                    ForeColor = Color.White   
                };
                btnCacTap.Click += (s, e) =>
                {
                    foreach (Button btn in pnlSoTap.Controls)
                    {
                        btn.BackColor = Color.Gray;
                    }
                    btnCacTap.BackColor = Color.Orange;
                    String video = tap.vidPathTap;
                    PhatVideo playVideo = new PhatVideo();
                    playVideo.setVideo(video);
                    playVideo.Show();
                };

                pnlSoTap.Controls.Add(btnCacTap);  
            }
        }

        private void XemChiTietUser_Load(object sender, EventArgs e)
        {
                LoadDanhSachTap();
        }
        public void SetMovieDetails(Movie_model movie)
        {
            if (movie == null)
            {
                MessageBox.Show("Dữ liệu không hợp lệ", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (lblTenPhim != null) lblTenPhim.Text = movie.Title;
            if (lbStar != null) lbStar.Text = movie.Rating.ToString();
            if (lblNam != null) lblNam.Text = movie.Year.ToString();
            if (lblDaoDien != null) lblDaoDien.Text = movie.Director;
            if (lblDienVien != null) lblDienVien.Text = movie.Cast;
            if (lblTheLoai != null) lblTheLoai.Text = movie.Genre != null ? movie.Genre.GenreName : "N/A"; 
            if (lblDinhDang != null) lblDinhDang.Text = movie.Format != null ? movie.Format.FormatName : "N/A";
            if (lblQuocGia != null) lblQuocGia.Text = movie.Country != null ? movie.Country.CountryName : "N/A";
            if (lblMota != null) lblMota.Text = movie.Description;

            filePath = movie.ImgPath;
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                var img = Image.FromFile(filePath);
                var resizedImg = new Bitmap(img, pbImage.Width, pbImage.Height);
                pbImage.Image = resizedImg;
            }
            else
            {
                MessageBox.Show("Không tìm thấy tệp hình ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            vidPath = movie.VidPath;
        }

        private void btnTap_Click_1(object sender, EventArgs e)
        {
            if (episode_Controller.checkTap(idPhim))
                {
                    pnlSoTap.Visible = !pnlSoTap.Visible;
                }
                else
                {
                    MessageBox.Show("Không có tập nào!");
                }
                if (pnlSoTap.Visible)
                {
                    btnTap.Text = "Ẩn danh sách";
                }
                else
                {
                    btnTap.Text = "Xem tập";
                }
        }

        private void LoadReview(int movieId)
        {
            try
            {
                List<DanhGia_model> dsDanhgia = danhgiaController.GetReviewUser(movieId);
                if (dsDanhgia != null && dsDanhgia.Count > 0)
                {
                    pnlReview.Controls.Clear();
                    foreach (DanhGia_model danhgia in dsDanhgia)
                    {
                        ItemReviewformUser item = new ItemReviewformUser(movie, danhgia, formMainUser, user);
                        pnlReview.Controls.Add(item);
                    }
                    pnlReview.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetStarRating(int rating)
        {
            soStar = rating;
            pbStar1.Image = (rating >= 1) ? Resources.star : Resources.stargray;
            pbStar2.Image = (rating >= 2) ? Resources.star : Resources.stargray;
            pbStar3.Image = (rating >= 3) ? Resources.star : Resources.stargray;
            pbStar4.Image = (rating >= 4) ? Resources.star : Resources.stargray;
            pbStar5.Image = (rating >= 5) ? Resources.star : Resources.stargray;
        }
        private void pbStar1_Click(object sender, EventArgs e)
        {
            SetStarRating(1);
        }

        private void pbStar2_Click(object sender, EventArgs e)
        {
            SetStarRating(2);
        }

        private void pbStar3_Click(object sender, EventArgs e)
        {
            SetStarRating(3);
        }

        private void pbStar4_Click(object sender, EventArgs e)
        {
            SetStarRating(4);
        }

        private void pbStar5_Click(object sender, EventArgs e)
        {
            SetStarRating(5);
        }
        private void insertReview(int movieID, int userID)
        {
            btnThem.Click += new EventHandler((sender, e) =>
            {
                pnlDanhGia.Visible = true;
            });
            btnLuu.Click += new EventHandler((sender, e) =>
            {
                try
                {
                    int rating = soStar;
                    string comment = txtCmt.Text.Trim();
                    DateTime time = DateTime.Now;
                    danhgiaController.InsertReview(movieID, userID, rating, comment, time);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                pnlDanhGia.Visible = true;
                ShowData(movieID);
            });

        }
        public void InsertFavorite(int movieId, int userId)
        {
            btnFavourite.Click -= BtnFavorite_Click;
            btnFavourite.Click += BtnFavorite_Click;

            void BtnFavorite_Click(object sender, EventArgs e)
            {
                try
                {
                    if (favouriteController.CheckMovie(movieId, userId))
                    {
                        MessageBox.Show("Phim đã được thêm vào danh sách trước đó");
                    }
                    else
                    {
                        if (favouriteController.InsertFavorite(movieId, userId))
                        {
                            MessageBox.Show("Phim đã được thêm vào danh sách");
                            btnFavourite.BackColor = Color.FromArgb(5, 38, 89);
                        }
                        else
                        {
                            MessageBox.Show("Không thể thêm phim vào danh sách yêu thích");
                        }
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void CheckMovieFavorite(int movieId, int userId)
        {
            try
            {
                if (favouriteController.CheckMovie(movieId, userId))
                {
                    btnFavourite.BackColor = Color.FromArgb(5, 38, 89);
                }
                else
                {
                    btnFavourite.BackColor = Color.FromArgb(125, 160, 202);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ShowData(int movieId)
        {
            try
            {
                Movie_model movie = controller.GetMovieById(movieId);
                if (movie != null)
                {
                    SetMovieDetails(movie);
                    LoadReview(movieId);
                }
                else
                {
                    Console.WriteLine("Movie with ID not found!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error with query: " + ex.Message);
            }
        }

        public void setShowBtnBackFavourite(bool a)
        {
            btnBackFavourite.Visible = a;
        }
        public void setShowBtnBackTrangChu(bool a)
        {
            btnBackTrangChu.Visible = a;
        }
        private void btnBackTrangChu_Click(object sender, EventArgs e)
        {
            formMainUser.OpenChidForm(new View.View_Container.TrangchuUser(formMainUser), sender);
        }

        private void btnBackFavourite_Click(object sender, EventArgs e)
        {
            formMainUser.OpenChidForm(new View.View_Container.FormFavourite(formMainUser),sender);
        }

        public void showMovie(int movieId, int userId)
        {
            idPhim = movieId;
            try
            {
                Movie_model movie = controller.GetMovieById(movieId);
                if (movie != null)
                {
                    SetMovieDetails(movie);
                    LoadReview(movieId);
                    insertReview(movieId, userId);
                    InsertFavorite(movieId, userId);
                    CheckMovieFavorite(movieId, userId);
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
