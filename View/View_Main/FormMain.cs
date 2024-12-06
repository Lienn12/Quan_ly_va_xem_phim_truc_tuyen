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
        }

        private void pnlMenu_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rc = pnlMenu.ClientRectangle;
            using (LinearGradientBrush brush = new LinearGradientBrush(
               rc,
               Color.FromArgb(28, 181, 224),
               Color.FromArgb(0, 0, 70),
               LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rc);
            }
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
            childForm.Dock=DockStyle.Fill;
            this.pnlMain.Controls.Add(childForm);   
            this.pnlMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            txtName.Text = childForm.Text;
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
            OpenChidForm(new View_Container.FormTrangChu(), sender);
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Login_Signup.FormLogin(), sender);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.FormTrangChu(), sender);
        }

        private void pnlMain_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
