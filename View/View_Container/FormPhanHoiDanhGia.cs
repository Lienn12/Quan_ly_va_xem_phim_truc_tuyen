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
    public partial class FormPhanHoiDanhGia : Form
    {
        private int reviewId; // luu thong tin id danh gia duoc truyen vao 
        public FormPhanHoiDanhGia(int reviewId)
        {
            InitializeComponent();
            this.reviewId = reviewId; // luuu gia tri truyn vao
        }

        private void btnTru_Click(object sender, EventArgs e)
        {

        }

        private void FormPhanHoiDanhGia_Load(object sender, EventArgs e)
        {
            // Sử dụng reviewId để hiển thị thông tin hoặc thực hiện logic cần thiết
            MessageBox.Show($"ID đánh giá đang xem: {reviewId}", "Thông Tin");
        }
    }
}
