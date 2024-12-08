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
    public partial class FormLoginSignup : Form
    {
        private Form activeForm;
        public FormLoginSignup()
        {
            InitializeComponent();
            OpenChidForm(new View_Login_Signup.FormLogin(this));

        }
        public void OpenChidForm(Form childForm)
        {

            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.Dock = DockStyle.Fill;
            childForm.FormBorderStyle = FormBorderStyle.None;
            pnlLogSign.Controls.Add(childForm);
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
    }
}
