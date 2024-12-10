using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class FormDSPhim : Form
    {
        private FormMain formMain;
        private SuaPhim suaPhim;
        private XemChiTiet chiTiet;
        public FormDSPhim(FormMain formMain)
        {
            this.formMain = formMain;
            InitializeComponent();
            this.Load += FormDSPhim_Load; // Gắn sự kiện Load cho form
        }
        private void LoadMovies()
        {
            try
            {
                // Tạo controller và lấy danh sách phim
                Movie_controller movieController = new Movie_controller();
                List<Movie_model> movies = movieController.GetMovies();

                // Xóa dữ liệu cũ
                dataGridView1.Rows.Clear();

                // Thêm từng dòng dữ liệu vào DataGridView
                foreach (var movie in movies)
                {
                    dataGridView1.Rows.Add(movie.MovieId, movie.Title, movie.Year);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu phim: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormDSPhim_Load(object sender, EventArgs e)
        {
            // Đảm bảo DataGridView không tự động tạo cột
            dataGridView1.AutoGenerateColumns = false;

            // Gọi hàm tải dữ liệu phim
            LoadMovies();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            formMain.OpenChidForm(new View.FormThemPhim(formMain),sender);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Chỉ xử lý khi nhấn vào hàng hợp lệ
            {
                int movieId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value); // Lấy MovieId
                string movieTitle = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (e.ColumnIndex == 3) // Cột 3: Chuyển đến trang chi tiết
                {
                    chiTiet = formMain.getChiTiet();
                    chiTiet.showMovie(movieId);
                    formMain.OpenChidForm(chiTiet, sender);
                    
                }
                else if (e.ColumnIndex == 4) // Cột 4: Chuyển đến trang sửa phim
                {
                    int id = movieId;
                    suaPhim = formMain.GetSuaPhim();
                    suaPhim.editPhim(id);
                    formMain.OpenChidForm(suaPhim, sender);  
                }
                else if (e.ColumnIndex == 5) // Cột 5: Xóa
                {
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa phim: {movieTitle}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            Movie__controller movieController = new Movie__controller();
                            bool isDeleted = movieController.DeleteFilm(movieId);

                            if (isDeleted)
                            {
                                MessageBox.Show("Xóa phim thành công!", "Thông báo");
                                LoadMovies(); // Tải lại danh sách phim
                            }
                            else
                            {
                                MessageBox.Show("Xóa phim thất bại. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi xóa phim: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void SearchPhim(string keyword)
        {
            // Lấy danh sách phim từ controller
            Movie__controller movieController = new Movie__controller();
            List<Movie_model> movieList = movieController.GetMovies();  // Lấy tất cả dữ liệu phim

            // Nếu từ khóa không rỗng, lọc danh sách
            if (!string.IsNullOrEmpty(keyword))
            {
                movieList = movieList.Where(movie =>
                    movie.MovieId.ToString().Contains(keyword) || // Tìm theo mã phim
                    movie.Title.ToLower().Contains(keyword.ToLower())  // Tìm theo tên phim
                    //movie.Year.ToString().Contains(keyword) || // Tìm theo năm
                    //movie.Director.ToLower().Contains(keyword.ToLower()) || // Tìm theo đạo diễn
                    //movie.Cast.ToLower().Contains(keyword.ToLower()) || // Tìm theo dàn diễn viên
                    //movie.Genre.GenreName.ToLower().Contains(keyword.ToLower()) || // Tìm theo thể loại
                    //movie.Format.FormatName.ToLower().Contains(keyword.ToLower()) || // Tìm theo định dạng
                    //movie.Country.CountryName.ToLower().Contains(keyword.ToLower()) // Tìm theo quốc gia
                ).ToList();
            }

            // Cập nhật DataGridView
            dataGridView1.DataSource = null;  // Xóa dữ liệu cũ
            dataGridView1.DataSource = movieList;  // Cập nhật lại dữ liệu sau khi lọc
        }

        private void txtTimkiem_TextChanged(object sender, EventArgs e)
        {
            SearchPhim(txtTimkiem.Text);
        }

        private void txtTimkiem_Leave(object sender, EventArgs e)
        {
            Movie__controller movieController = new Movie__controller();
            List<Movie_model> movieList = movieController.GetMovies(); 
            if (string.IsNullOrWhiteSpace(txtTimkiem.Text))
            {
                txtTimkiem.Text = "Tìm kiếm phim";
                txtTimkiem.ForeColor = Color.Gray; // Đổi màu chữ khi lại có văn bản mặc định
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = movieList;
        }

        private void txtTimkiem_Enter(object sender, EventArgs e)
        {
            if (txtTimkiem.Text == "Tìm kiếm phim")
            {
                txtTimkiem.Text = "";
                txtTimkiem.ForeColor = Color.Black; // Đổi màu chữ nếu cần
            }
        }
    }
}
