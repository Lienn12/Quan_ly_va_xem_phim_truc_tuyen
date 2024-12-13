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
    public partial class ItemReviewformUser : UserControl
    {
        private FormMainUser formMainUser;
        private Movie_model movieModel;
        private User_model userModel;
        private DanhGia_model danhgia;
        public ItemReviewformUser(Movie_model movieModel, DanhGia_model danhgia, FormMainUser formMainUser, User_model userModel)
        {
            this.movieModel = movieModel;
            this.userModel = userModel;
            this.formMainUser = formMainUser;
            InitializeComponent();
            lbUser.Text = danhgia.User.username;
            lbRating.Text = danhgia.Rating.ToString();
            txtCmt.Text = danhgia.Comment;
            txtReply.Text = danhgia.Reply;
        }
    }
}
