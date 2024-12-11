using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace Quan_ly_thu_vien_phim.View.View_Container
{

    public partial class FormPhanHoiDanhGia : Form
    {
        private DanhGia_model danhgia ; 
        private DanhGia_controller danhgiaController ;
        private FormMain formMain;
        private string filePath;
        public FormPhanHoiDanhGia(FormMain formMain)
        {
            InitializeComponent();
            this.formMain = formMain;
            LoadIcons();
            danhgiaController = new DanhGia_controller();
            setShow(false);
        }

         public void setShow(bool a)
        {
            txtPhanhoi.Visible = a;
            save.Visible = a;
        }
        public void SetData(DanhGia_model danhgia)
        {
            lbUsername.Text = danhgia.User.username;
            string formattedDate = danhgia.ReviewDate.ToString("dd/MM/yyyy HH:mm:ss");
            lbDate.Text = formattedDate;
            txtBinhluan.Text = danhgia.Comment;
            txtShowReply.Text = danhgia.Reply;
            title.Text = danhgia.Movie.Title;
            filePath = danhgia.Movie.ImgPath;
            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                var img = Image.FromFile(filePath);
                var resizedImg = new Bitmap(img, Anhphim.Width, Anhphim.Height);
                Anhphim.Image = resizedImg;
            }
            else
            {
                MessageBox.Show("Không tìm thấy tệp hình ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            SetRating(danhgia.Rating);
            btnThem.Click += (sender, e) =>
            {
                setShow(true);
            };
            btnTru.Click += (sender, e) =>
            {
                danhgiaController.DeleteReply(danhgia.ReviewId);
                txtShowReply.Text = "";
            };
            save.Click += (sender, e) =>
            { 
                string reply = txtPhanhoi.Text;
                if (string.IsNullOrEmpty(reply)) 
                {
                    MessageBox.Show("Vui lòng nhập phản hồi");
                }
                if(danhgiaController.SetReply(danhgia.ReviewId, reply))
                {
                    txtShowReply.Text = reply;
                    txtPhanhoi.Clear();
                    setShow(false);
                }

            };


        }
        public void ShowDanhgia(int reviewId)
        {
            try
            {
                danhgia = danhgiaController.GetReply(reviewId);
                if (danhgia != null)
                {
                    SetData(danhgia);
                }
                else
                {
                    MessageBox.Show(this, "No user found with ID: " + reviewId, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show( "Error while showing review data." + ex.Message);
            }
        }

        private Image emptyStar, fullStar;

        private void LoadIcons()
        {
            emptyStar = Properties.Resources.stargray;  
            fullStar = Properties.Resources.star;    
        }

        private void btnQuaylai_Click(object sender, EventArgs e)
        {
            formMain.OpenChidForm(new View.View_Container.FormDanhGia(formMain), sender);
        }

        private void SetRating(int rating)
        {
            star1.Image = rating >= 1 ? fullStar : emptyStar;
            star2.Image = rating >= 2 ? fullStar : emptyStar;
            star3.Image = rating >= 3 ? fullStar : emptyStar;
            star4.Image = rating >= 4 ? fullStar : emptyStar;
            star5.Image = rating >= 5 ? fullStar : emptyStar;
        }

    }
}
