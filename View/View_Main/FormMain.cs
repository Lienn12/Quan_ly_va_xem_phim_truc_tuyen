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
        private Form activeForm;
        public FormMain()
        {
            InitializeComponent();
            this.Size = new Size(1000, 600);
            pnlMenu.Size = new Size(200, 600);
            pnlMenu.Dock = DockStyle.Left;
            pnlMain.AutoScroll = true;
            pnlMain.Dock = DockStyle.Fill;
        }

        private void OpenChidForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm=childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            this.pnlMain.Controls.Add(childForm);   
            this.pnlMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.FormTrangChu(), sender);
        }

        private void btnFilm_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.FormTrangChu(), sender);
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.FormDSNguoiDung(), sender);
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Login_Signup.FormLogin(), sender);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Main.FormLoginSignup(), sender);
        }
    }
}
