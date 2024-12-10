using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Quan_ly_thu_vien_phim.View.View_Container
{

    public partial class FormPhanHoiDanhGia : Form
    {
        private FormMain formMain;
        public FormPhanHoiDanhGia(FormMain formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
        }

        private void btnTru_Click(object sender, EventArgs e)
        {

        }

        private void FormPhanHoiDanhGia_Load(object sender, EventArgs e)
        {
           
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //int reviewId = 1; // Lấy ID đánh giá hiện tại
            //string replyContent = txtPhanHoi.Text;

            //if (!string.IsNullOrEmpty(replyContent))
            //{
            //    PhanHoiDanhGia_controller controller = new PhanHoiDanhGia_controller();
            //    controller.AddReply(reviewId, replyContent);

            //    MessageBox.Show("Phản hồi đã được thêm thành công!");
            //}
        }
    }
}
