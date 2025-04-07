using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Container;
using Quan_ly_thu_vien_phim.View.View_Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View
{
    public partial class FormMain : Form
    {
        private SuaPhim suaPhim ;
        private XemChiTiet xemChiTiet;
        private Form activeForm;
        private FormPhanHoiDanhGia formPhanHoi;
        private FormMainUser formMainUser;
        public FormMain()
        {
            InitializeComponent();
            lbMinimum.Click += btnMinimum_Click;
            lbExit.Click += btnExit_Click;
            this.Size = new Size(1250, 800);
            pnlHeader.Size = new Size(1250, 45);
            pnlMenu.Size = new Size(250, 760);
        }

        public void OpenChidForm(Form childForm, object btnSender)
        {

            if (this.activeForm != null && !activeForm.IsDisposed)
            {
                activeForm.Close();
            }

            this.activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            Panel scrollablePanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true 
            };
            this.pnlMain.Controls.Clear();
            this.pnlMain.Controls.Add(scrollablePanel); 
            scrollablePanel.Controls.Add(childForm);
            childForm.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.FormTrangChu(this), sender);
        }

        private void btnFilm_Click(object sender, EventArgs e)
        {
            menutranstion.Start();
            OpenChidForm(new View_Container.FormDSPhim(this), sender);
        }
         private void btnQuocGia_Click(object sender, EventArgs e)
         {

            OpenChidForm(new View_Container.frmQuocGia(), sender);
        }
        private void btnTheLoai_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.frmTheLoai(), sender);
        }
        private void btnUser_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.FormDSNguoiDung(), sender);
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.FormDanhGia(this), sender);
        }
        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            menuPay.Start();
        }
        private void btnGoi_Click(object sender, EventArgs e)
        {

            OpenChidForm(new View_Container.frmGoiDichVu(), sender);
        }
        private void btnPT_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.formPhuongThuc(), sender);
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormLoginSignup formLoginSignup = new FormLoginSignup();
            this.Hide(); 
            formLoginSignup.ShowDialog(); 
            this.Close();
        }
        private void btnMinimum_Click (object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Close();
        }
        public SuaPhim GetSuaPhim()
        {
            if (suaPhim == null || suaPhim.IsDisposed)
            {
                suaPhim = new SuaPhim(this);
            }
            return suaPhim;
        }
        public XemChiTiet getChiTiet()
        {
            if (xemChiTiet == null || xemChiTiet.IsDisposed)
            {
                xemChiTiet = new XemChiTiet(this);
            }
            return xemChiTiet;
        }
        public FormPhanHoiDanhGia GetDanhGia()
        {
            if (formPhanHoi == null || formPhanHoi.IsDisposed)
            {
                formPhanHoi = new FormPhanHoiDanhGia(this);
            }
            return formPhanHoi;
        }
        public void ShowMovieDetail(Movie_model movie, object sender)
        {

            if (xemChiTiet == null || xemChiTiet.IsDisposed)
            {
                xemChiTiet = new XemChiTiet(this);
            }
            xemChiTiet.showMovie(movie.MovieId);
            xemChiTiet.setShowBtnBackTrangChu(true);
            xemChiTiet.setShowBtnBackPhim(false);
            OpenChidForm(xemChiTiet, sender);
        }
        bool menuExpand = false;
        private void menutranstion_Tick(object sender, EventArgs e)
        {
            if (menuExpand == false)
            {
                menucontainer.Height += 30;
                if(menucontainer.Height>= 130)
                {
                    menutranstion.Stop();
                    menuExpand=true;
                }
            }
            else
            {
                menucontainer.Height -= 30;
                if(menucontainer.Height<=50)
                {
                    menutranstion.Stop();
                    menuExpand=false;
                }
            }
        }
        bool menuExpandPay = false;
        private void menuPay_Tick(object sender, EventArgs e)
        {
            if (menuExpandPay == false)
            {
                ThanhToan.Height += 30;
                if (ThanhToan.Height >= 130)
                {
                    menuPay.Stop();
                    menuExpandPay = true;
                }
            }
            else
            {
                ThanhToan.Height -= 30;
                if (ThanhToan.Height <= 50)
                {
                    menuPay.Stop();
                    menuExpandPay = false;
                }
            }
        }

       
    }
}
