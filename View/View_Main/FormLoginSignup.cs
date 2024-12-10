using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Login_Signup;
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
        private Formsignup formsignup;
        private User_model user;
        private FormForgotPassword FormForgotPassword;
        private FormReset FormReset;
        public FormLoginSignup()
        {
            InitializeComponent();
            OpenChidForm(new View_Login_Signup.FormLogin(this));
            formsignup = new Formsignup(this, user);
            FormForgotPassword= new FormForgotPassword(this);
            user = new User_model();
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
        public void ShowVerifyCode(User_model user)
        {
            VerifyCode verifyCodeForm = new VerifyCode(this, user);
            verifyCodeForm.Show();
        }
        public void showForgot(User_model user) 
        {
            FormForgotPassword = new FormForgotPassword(this);
            FormForgotPassword.setUserModel(user);
            OpenChidForm(FormForgotPassword);
        }
        public void showReset(User_model user)
        {
            FormReset = new FormReset(this);
            FormReset.setUserModel(user);
            OpenChidForm(FormReset);
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
