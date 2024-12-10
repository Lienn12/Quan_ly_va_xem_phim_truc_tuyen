using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class SuaPhim : Form
    {
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
                MessageBox.Show("Đường dẫn video: " + videoPath);

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
                        MessageBox.Show("GenreID: " + genreId);
                        MessageBox.Show("cbTheLoai.Items[i]: " + cbTheLoai.Items[i]);
                        cbTheLoai.SelectedIndex = i;
                        break;
                    }
                }
            }
            int formatId = movie.Format.FormatID;
            for (int i = 1; i < cbDinhDang.Items.Count; i++)
            {
                if (cbDinhDang.Items[i] is Format_model format && format.FormatID == formatId)
                {
                    cbDinhDang.SelectedIndex = i;
                    break;
                }
            }
            int countryId = movie.Country.CountryId;
            for (int i = 1; i < cbQuocGia.Items.Count; i++)
            {
                if (cbQuocGia.Items[i] is Country_model country && country.CountryId == countryId)
                {
                    cbQuocGia.SelectedIndex = i;
                    break;
                }
            }
            MessageBox.Show(movie.Genre.ToString());
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
                else
                {
                    Console.WriteLine("Không tìm thấy Video.");
                }
            }
            else
            {
                Console.WriteLine("Không có video trong Phim.");
            }
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
    }
}
