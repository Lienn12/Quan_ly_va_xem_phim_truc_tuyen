using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_useControl
{
    public partial class ItemReviewUser : UserControl
    {
        private FormMain formMain;
        private Movie_model movieModel;
        private User_model userModel;
        private DanhGia_model danhgia;
        public ItemReviewUser(Movie_model movieModel, DanhGia_model danhgia, FormMain formMain, User_model userModel)
        {
            this.movieModel = movieModel;
            this.userModel = userModel;
            this.formMain = formMain;
            this.formMain = formMain;
            InitializeComponent();  
            lbUser.Text=danhgia.User.username;
            lbRating.Text=danhgia.Rating.ToString();
            txtCmt.Text = danhgia.Comment;
            if (danhgia.Reply == null)
            {
                txtReply.Visible=false;
                lbAdmin.Visible=false;
            }
            else
            {
                txtReply.Text = danhgia.Reply;
            }
            
        }
    }
}
