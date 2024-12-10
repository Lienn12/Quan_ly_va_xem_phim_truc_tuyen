using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_Main
{
    public partial class FormMainUser : Form
    {
        private Form activeForm;
        public FormMainUser()
        {
            InitializeComponent();
            this.Size = new Size(1250, 800);
            pnlHeader.Size = new Size(1250, 40);
            pnlMenu.Size = new Size(250, 760);
        }

        public void OpenChidForm(Form childForm, object btnSender)
        {

            if (activeForm != null)
            {
                activeForm.Close();
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
            OpenChidForm(new View_Container.TrangUser(),sender);
        }

        private void btnFavourite_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.TrangUser(), sender);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            OpenChidForm(new View_Container.FormTrangChu(), sender);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            FormLoginSignup formLoginSignup = new FormLoginSignup();
            this.Hide();
            formLoginSignup.ShowDialog();
            this.Close();
        }
    }
}
