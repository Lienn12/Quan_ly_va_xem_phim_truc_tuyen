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
                Movie__controller movieController = new Movie__controller();
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
            try
            {
                if (e.RowIndex >= 0) // Chỉ xử lý khi nhấn vào hàng hợp lệ
                {
                    int movieId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value); // Lấy MovieId
                    string movieTitle = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    if (e.ColumnIndex == 3) // Cột 3: Chuyển đến trang chi tiết
                    {
                        if (chiTiet == null || chiTiet.IsDisposed)
                            chiTiet = formMain.getChiTiet();
                        chiTiet.showMovie(movieId);
                        formMain.OpenChidForm(chiTiet, sender);

                    }
                    else if (e.ColumnIndex == 4) // Cột 4: Chuyển đến trang sửa phim
                    {
                        if (suaPhim == null || suaPhim.IsDisposed)
                            suaPhim = formMain.GetSuaPhim();
                        suaPhim.editPhim(movieId);
                        formMain.OpenChidForm(suaPhim, sender);
                    }
                    //else if (e.ColumnIndex == 5) // Cột 5: Xóa
                    //{
                    //    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa phim: {movieTitle}?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    //    if (result == DialogResult.Yes)
                    //    {
                    //        Movie__controller movieController = new Movie__controller();
                    //        // Gọi hàm xóa trong controller
                    //        if (movieController.DeleteMovie(movieId))
                    //        {
                    //            MessageBox.Show("Xóa phim thành công!", "Thông báo");
                    //            LoadMovies(); // Tải lại danh sách phim
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Xóa phim thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
