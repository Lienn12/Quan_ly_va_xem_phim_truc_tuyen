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
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class SuaPhim : Form
    {

        private FormMain formMain;
        private FormDSPhim dsPhim;
        private string filePath, videoPath;
        private Movie_controller movie_Controller = new Movie_controller();
        private Genre_controller genre_Controller = new Genre_controller();
        private Format_controller format_Controller = new Format_controller();
        private Country_controller country_Controller = new Country_controller();
        private Genre_model genre = new Genre_model();
        private Country_model country = new Country_model();
        private Format_model format = new Format_model();
        private int newMovieId;
        public SuaPhim(FormMain formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
            dsPhim = new FormDSPhim(formMain);
            LoadDataComboGenres(cbTheLoai, genre_Controller.GetGenres());
            LoadDataComboFormats(cbDinhDang, format_Controller.GetFormats());
            LoadDataComboCountries(cbQuocGia, country_Controller.GetCountries());
        }

        private void btnVid_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog   // Tạo OpenFileDialog
            {
                Filter = "Video Files|*.mp4;*.avi;*.wmv;*.mov;*.mkv",
                Title = "Chọn video"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)   // Hiển thị hộp thoại chọn file
            {
                // Lấy đường dẫn file video được chọn
                videoPath = openFileDialog.FileName;
                // Hiển thị đường dẫn ra màn hình (MessageBox hoặc TextBox)
                btnVid.BackColor = Color.Green;

            }
            else
            {
                // Người dùng bấm Cancel
                MessageBox.Show("Bạn chưa chọn file nào!");
            }
        }

        private void SuaPhim_Paint(object sender, PaintEventArgs e)
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

        private void btnBack_Click(object sender, EventArgs e)
        {
            formMain.OpenChidForm(new View.View_Container.FormDSPhim(formMain), sender);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtSoTap.Text, out int numberOfEpisodes) && numberOfEpisodes > 0)
            {
                // Mở Form Thêm Tập và truyền số lượng tập
                ThemTap addEpisodesForm = new ThemTap(numberOfEpisodes,newMovieId, txtTen.Text);
                addEpisodesForm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập số tập hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnImage_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Title = "Chọn ảnh để tải lên"
            };
            // Hiển thị hộp thoại chọn file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn file
                filePath = openFileDialog.FileName;
                // Hiển thị ảnh trong PictureBox
                pbMovie.Image = Image.FromFile(filePath);
                pbMovie.SizeMode = PictureBoxSizeMode.Zoom;
                btnImage.BackColor = Color.Green;
                // Xử lý bổ sung (nếu cần)
                MessageBox.Show("Đã tải lên ảnh thành công!");
            }
        }

        private void LoadDataComboBox<T>(ComboBox comboBox, List<T> dataList, string displayField)
        {
            try
            {
                comboBox.Items.Clear();
                if (dataList != null && dataList.Count > 0)
                {
                    foreach (var item in dataList)
                    {
                        if (item is Genre_model genreItem)
                        {
                            if (genreItem.GenreID == 1)
                                continue; // Skip items with GenreId == 1
                        }
                        else if (item is Format_model formatItem)
                        {
                            if (formatItem.FormatID == 1)
                                continue; // Skip items with FormatId == 1
                        }
                        else if (item is Country_model countryItem)
                        {
                            if (countryItem.CountryId == 1)
                                continue; // Skip items with CountryId == 1
                        }
                        comboBox.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Danh sách trống hoặc không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                comboBox.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadDataComboGenres(ComboBox comboBox, List<Genre_model> genreList)
        {
            LoadDataComboBox(comboBox, genreList, "GenreName"); // "GenreName" is the property for display
        }
        public void LoadDataComboFormats(ComboBox comboBox, List<Format_model> formatList)
        {
            LoadDataComboBox(comboBox, formatList, "FormatName"); // "FormatName" is the property for display
        }
        public void LoadDataComboCountries(ComboBox comboBox, List<Country_model> countryList)
        {
            LoadDataComboBox(comboBox, countryList, "CountryName"); // "CountryName" is the property for display
        }

        public void setMovie(Movie_model movie)
        {
            newMovieId = movie.MovieId;
            txtNam.Text = movie.Year.ToString();
            txtTen.Text = movie.Title;
            txtDaoDien.Text = movie.Director;
            txtDienVien.Text = movie.Cast;
            
            if (movie.Genre != null)
            {
                for (int i = 0; i < cbTheLoai.Items.Count; i++)
                {
                    if (cbTheLoai.Items[i] is Genre_model genre && genre.GenreName == movie.Genre.GenreName)
                    {
                        int genreId = genre.GenreID;
                        cbTheLoai.SelectedIndex = i;
                        break;
                    }
                }
            }
            if (movie.Format != null)
            {
                for (int i = 0; i < cbDinhDang.Items.Count; i++)
                {
                    if (cbDinhDang.Items[i] is Format_model format && format.FormatName == movie.Format.FormatName)
                    {
                        //int formatid = format.FormatID;
                        cbDinhDang.SelectedIndex = i;
                        break;
                    }
                }
            }
            if (movie.Country != null)
            {
                for (int i = 0; i < cbQuocGia.Items.Count; i++)
                {
                    if (cbQuocGia.Items[i] is Country_model country && country.CountryName == movie.Country.CountryName)
                    {
                        //int countryid = country.CountryId;
                        cbQuocGia.SelectedIndex = i;
                        break;
                    }
                }
            }
            
            txtSoTap.Text = movie.Episode.ToString();
            txtMota.Text = movie.Description;
            if (!string.IsNullOrEmpty(movie.ImgPath))
            {
                try
                {
                    if (File.Exists(movie.ImgPath))
                    {
                        var img = Image.FromFile(movie.ImgPath);
                        var resizedImg = new Bitmap(img, pbMovie.Width, pbMovie.Height);
                        pbMovie.Image = resizedImg;
                    }
                    else
                    {
                        Console.WriteLine("Không tìm thấy ảnh.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Lỗi hiển thị ảnh: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Không có ảnh trong Phim.");
            }
            if (!string.IsNullOrEmpty(movie.VidPath))
            {
                if (File.Exists(movie.VidPath))
                {
                    btnVid.BackColor = Color.Green;
                }
            }
            else
            {
                Console.WriteLine("Không có video trong Phim.");
            }
            filePath = movie.ImgPath;
            videoPath = movie.VidPath;
            btnSave.Click += (sender, e) =>
            {
                try
                {
                    Movie_model updatedMovie = GetMovieDetails();
                    updatedMovie.MovieId = newMovieId;

                    if (movie_Controller.UpdateMovie(updatedMovie))
                    {
                        MessageBox.Show("Đã sửa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Sửa không thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                            };
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setNull();
        }

        public void editPhim(int movieID )
        {
            try
            {
                Movie_model movie = movie_Controller.GetMovieById(movieID);
                if (movie != null)
                {
                    setMovie(movie);
                   
                }
                else
                {
                    Console.WriteLine("Không tìm thấy movie.");
                }
            }catch (Exception e)
            {
                MessageBox.Show(movieID+$"Lỗi: {e.Message}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Movie_model GetMovieDetails()
        {
            // Tạo một đối tượng Movie_model
            Movie_model movie = new Movie_model
            {
                Genre = new Genre_model(),
                Country = new Country_model(),
                Format = new Format_model()
            };
            // Lấy tên phim
            movie.Title = txtTen.Text;
            // Lấy năm phát hành
            if (!int.TryParse(txtNam.Text, out int releaseYear) || releaseYear < 0 || releaseYear > 2024)
            {
                MessageBox.Show("Vui lòng nhập năm hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            movie.Year = releaseYear;
            // Lấy thông tin đạo diễn và diễn viên
            movie.Director = txtDaoDien.Text;
            movie.Cast = txtDienVien.Text;
            // Lấy thể loại
            if (cbTheLoai.SelectedItem is Genre_model genre)
            {
                movie.Genre = genre;
            }
            // Lấy định dạng
            if (cbDinhDang.SelectedItem is Format_model format)
            {
                movie.Format = format;
            }
            // Lấy quốc gia
            if (cbQuocGia.SelectedItem is Country_model country)
            {
                movie.Country = country;
            }
            // Lấy số tập
            if (!int.TryParse(txtSoTap.Text, out int episodes) || episodes < 0)
            {
                MessageBox.Show("Số tập phải lớn hơn hoặc bằng 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            movie.Episode = episodes;
            // Lấy mô tả
            movie.Description = txtMota.Text;
            // Lấy đường dẫn ảnh
            if (!string.IsNullOrEmpty(filePath))
            {
                movie.ImgPath = filePath; // Gán filePath (chuỗi đường dẫn) vào ImgPath
            }
            // Lấy đường dẫn video
            if (!string.IsNullOrEmpty(videoPath))
            {
                movie.VidPath = videoPath; // Gán videoPath (chuỗi đường dẫn) vào VidPath
            }
            return movie;
        }

        private void cbDinhDang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDinhDang.SelectedItem is Format_model selected)
            {
                string formatName = selected.FormatName;
                if (formatName == "Phim lẻ")
                {
                    txtSoTap.Text = "1";
                    txtSoTap.Enabled = false;
                }
                else if (formatName == "Phim bộ")
                {
                    txtSoTap.Enabled = true;
                    txtSoTap.Text = string.Empty;
                }
            }
        }

        public void setNull()
        {
            this.txtTen = null;
            this.txtNam = null;
            this.txtDaoDien = null;
            this.txtDienVien = null;
            this.cbQuocGia = null;
            this.cbDinhDang = null;
            this.cbTheLoai = null;
            this.txtSoTap = null;
            this.txtMota = null;
            this.pbMovie = null;
            this.btnVid.BackColor = Color.LightSkyBlue;
        }
    }
}
