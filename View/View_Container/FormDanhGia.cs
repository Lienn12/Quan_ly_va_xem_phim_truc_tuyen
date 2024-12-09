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
                // Lấy danh sách đánh giá từ controller
                List<DanhGia_model> reviews = danhGia_Controller.getdanhgia();

                // Xóa dữ liệu cũ (nếu có)
                dataGridView1.Rows.Clear();

                // Thêm dữ liệu vào DataGridView
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
            formMain.OpenChidForm(new View.View_Container.FormPhanHoiDanhGia(formMain), sender);
                   
        }
    }
    
}
