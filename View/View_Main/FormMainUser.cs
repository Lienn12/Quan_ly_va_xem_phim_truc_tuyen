using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Container;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Collections.Specialized.BitVector32;

namespace Quan_ly_thu_vien_phim.View.View_Main
{
    public partial class FormMainUser : Form
    {
        private Form activeForm;
        private FormFavourite formFavourite;
        private XemChiTietUser xemChiTietUser;
        private TrangUser trangUser;
        private User_model user;
        private ThanhToan thanhToan;
        public FormMainUser()
        {
            InitializeComponent();
            this.Size = new Size(1250, 800);
            pnlHeader.Size = new Size(1250, 45);
            pnlMenu.Size = new Size(250, 760);
            
            if (trangUser == null || trangUser.IsDisposed)
            {
                trangUser = new TrangUser(this);
            }
            if (xemChiTietUser == null || xemChiTietUser.IsDisposed)
            {
                xemChiTietUser = new XemChiTietUser(this);
            }
            if(thanhToan == null || thanhToan.IsDisposed)
            {
                thanhToan = new ThanhToan(this);
            }
            if (Session.CurrentUser == null)
            {
                btnLogin.Text = "      Log in";
            }
            OpenChidForm(new View_Container.TrangchuUser(this), null);
        }

        public void OpenChidForm(Form childForm, object btnSender)
        {

            if (activeForm != null)
            {
                activeForm.Hide();
            }
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            Panel scrollablePanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(scrollablePanel);
            scrollablePanel.Controls.Add(childForm);
            childForm.Show();
        }
        private void lbExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void lbMinimum_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnCaNhan_Click(object sender, EventArgs e)
        {
            OpenChidForm(trangUser, sender);
        }

        private void btnFavourite_Click(object sender, EventArgs e)
        {
            OpenChidForm(new FormFavourite(this), sender);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.TrangchuUser(this), sender);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            
                OpenChidForm(new View_Container.FormGoiNguoiDung(this), sender);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormLoginSignup formLoginSignup = new FormLoginSignup();
            this.Hide();
            formLoginSignup.ShowDialog();
            this.Close();
        }
        public FormFavourite GetFavourite()
        {
            if (formFavourite == null || formFavourite.IsDisposed)
            {
                formFavourite = new FormFavourite(this);
            }
            return formFavourite;
        }
        public TrangUser getCaNhan()
        {
            
            return trangUser;
        }
        public ThanhToan getThanhToan()
        {
            return thanhToan;
        }
        public User_model getUserModel()
        {
            return user;
        }
        public void setuserModel(User_model user)
        {
            this.user = user;
        }
        public XemChiTietUser GetXemChiTiet()
        {
            
            return xemChiTietUser;
        }
        public void ShowMovieDetail(Movie_model movie, object sender)
        {
            if (xemChiTietUser == null || xemChiTietUser.IsDisposed)
            {
                xemChiTietUser = new XemChiTietUser(this);
            }
            if (user != null)
            {
                xemChiTietUser.showMovie(movie.MovieId, user.userId);
                xemChiTietUser.setShowBtnBackTrangChu(true);
                xemChiTietUser.setShowBtnBackFavourite(false);
                xemChiTietUser.setShowBtnFavourite(true);
            }
            else
            {
                xemChiTietUser.showMovie(movie.MovieId, -1);
                xemChiTietUser.setShowBtnBackTrangChu(true); 
                xemChiTietUser.setShowBtnBackFavourite(false);
                xemChiTietUser.setShowBtnFavourite(false); 
            }
            OpenChidForm(xemChiTietUser, sender);
        }

        private void FormMainUser_Load(object sender, EventArgs e)
        {
            UpdateLoginButtonText();
        }
        private void UpdateLoginButtonText()
        {
            btnLogin.Text = Session.CurrentUser == null ? "      Log in" : "      Log out";
        }
    }
}
