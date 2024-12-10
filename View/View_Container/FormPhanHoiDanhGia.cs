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
        private int selectedReviewId; // ID đánh giá hiện tại
        private FormMain formMain;
        public FormPhanHoiDanhGia(FormMain formMain, int reviewId)
        {
            InitializeComponent();
            this.formMain = formMain;
            this.selectedReviewId = reviewId;
        }

        private void btnTru_Click(object sender, EventArgs e)
        {
            
        }

        private void FormPhanHoiDanhGia_Load(object sender, EventArgs e)
        {
           LoadIcons();  // Gọi LoadIcons để tải hình ảnh sao
           LoadReviewDetails();
           
        }

        private void LoadReviewDetails()
        {
            PhanHoiDanhGia_controller controller = new PhanHoiDanhGia_controller();
            var details = controller.GetReviewDetails(selectedReviewId);

            if (details.Username != null)
            {
                txtUser.Text = details.Username;
                txtNgaydanhgia.Text = details.ReviewDate.ToString("dd/MM/yyyy");
                txtBinhluan.Text = details.Comment;
                txtPhanhoi1.Text = details.ReplyText;
                title.Text = details.title;
                //// Hiển thị rating bằng cách tô màu ngôi sao
                SetRating(details.Rating); // Gọi phương thức SetRating để hiển thị ngôi sao

                // Hiển thị ảnh phim
                if (!string.IsNullOrEmpty(details.ImgPath))
                {
                    if (File.Exists(details.ImgPath))
                    {
                        Anhphim.Image = Image.FromFile(details.ImgPath);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin đánh giá.");
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            // Đặt trạng thái ReadOnly của textbox về false để người dùng nhập vào
            txtPhanhoi1.ReadOnly = false;
            // Hiển thị thông báo hoặc đặt focus để hướng dẫn người dùng nhập
            MessageBox.Show("Bạn có thể nhập nội dung phản hồi.");
            txtPhanhoi1.Focus();

        }
        private Image emptyStar, fullStar;

        private void LoadIcons()
        {
            // Load hình ảnh sao trống và sao đầy
            emptyStar = Properties.Resources.stargray;  // Đảm bảo "empty_star" là tên của ảnh trong Resources
            fullStar = Properties.Resources.star;    // Đường dẫn tới biểu tượng sao đầy
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            // Quay lai form danh gia
            formMain.OpenChidForm(new View.View_Container.FormDanhGia(formMain), sender);
        }

        private void save_Click(object sender, EventArgs e)
        {
            // Khi người dùng đã nhập xong, xử lý nội dung nhập vào
            string replyContent = txtPhanhoi1.Text;

            if (!string.IsNullOrEmpty(replyContent))
            {
                PhanHoiDanhGia_controller controller = new PhanHoiDanhGia_controller();
                controller.AddReply(selectedReviewId, replyContent);

                MessageBox.Show("Phản hồi đã được thêm thành công!");

                // Đặt textbox về ReadOnly sau khi lưu phản hồi thành công (nếu cần)
                txtPhanhoi1.ReadOnly = true;
            }
            else
            {
                MessageBox.Show("Vui lòng nhập nội dung phản hồi.");
            }
        }

        private void SetRating(int rating)
        {
            // Giả sử bạn có 5 PictureBox cho các sao: star1, star2, star3, star4, star5
            star1.Image = rating >= 1 ? fullStar : emptyStar;
            star2.Image = rating >= 2 ? fullStar : emptyStar;
            star3.Image = rating >= 3 ? fullStar : emptyStar;
            star4.Image = rating >= 4 ? fullStar : emptyStar;
            star5.Image = rating >= 5 ? fullStar : emptyStar;
        }

    }
}
