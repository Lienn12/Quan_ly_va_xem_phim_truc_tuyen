using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class FormDanhGia : Form
    {
        private DanhGia_controller danhGia_Controller;
        private View.FormMain formMain;
        public FormDanhGia(FormMain formMain)
        {
            InitializeComponent();
            danhGia_Controller = new DanhGia_controller();
            this.formMain = formMain;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormDanhGia_Load(object sender, EventArgs e)
        {
            try
            {
                List<DanhGia_model> reviews = danhGia_Controller.getdanhgia();

                dataGridView1.Rows.Clear();

                foreach (var review in reviews)
                {
                    dataGridView1.Rows.Add(
                        review.ReviewId,       // Cột "ID Đánh Giá"
                        review.Username,       // Cột "Tên Tài Khoản"
                        review.MovieTitle,     // Cột "Tên Phim"
                        review.Rating,         // Cột "Rating"
                        review.ReviewDate      // Cột "Ngày Đánh Giá"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu đánh giá: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu nhấn vào một ô hợp lệ (không phải tiêu đề hoặc ô ngoài vùng dữ liệu)
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // Xử lý sự kiện khi nhấn vào cột "Phan_hoi"
            if (dataGridView1.Columns["Phan_hoi"] != null && e.ColumnIndex == dataGridView1.Columns["Phan_hoi"].Index)
            {
                try
                {
                    // Lấy giá trị của cột ReviewID
                    object reviewIdValue = dataGridView1.Rows[e.RowIndex].Cells["ReviewID"].Value;
                    if (reviewIdValue != null && int.TryParse(reviewIdValue.ToString(), out int reviewId) && reviewId > 0)
                    {
                        // Mở form PhanHoiDanhGia và truyền reviewId
                        formMain.OpenChidForm(new View.View_Container.FormPhanHoiDanhGia(formMain, reviewId), sender);
                    }
                    else
                    {
                        MessageBox.Show("Review ID không hợp lệ hoặc bị thiếu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi mở phản hồi: " + ex.Message);
                }
            }

            // Xử lý sự kiện khi nhấn vào cột "Delete"
            if (dataGridView1.Columns["Delete"] != null && e.ColumnIndex == dataGridView1.Columns["Delete"].Index)
            {
                try
                {
                    // Lấy giá trị của cột ReviewID
                    object reviewIdValue = dataGridView1.Rows[e.RowIndex].Cells["ReviewID"].Value;
                    if (reviewIdValue != null && int.TryParse(reviewIdValue.ToString(), out int reviewId) && reviewId > 0)
                    {
                        // Hiển thị hộp thoại xác nhận xóa
                        var confirmResult = MessageBox.Show("Bạn có chắc chắn muốn xóa phản hồi này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                        if (confirmResult == DialogResult.Yes)
                        {
                            // Gọi hàm xóa trong controller
                            DanhGia_controller controller = new DanhGia_controller();
                            controller.DeleteReview(reviewId);

                            // Xóa dòng khỏi DataGridView
                            dataGridView1.Rows.RemoveAt(e.RowIndex);

                            MessageBox.Show("Xóa phản hồi thành công!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Review ID không hợp lệ hoặc bị thiếu.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi xóa: " + ex.Message);
                }
            }

        }
    }
    
}
