using Quan_ly_thu_vien_phim.Controller;
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

namespace Quan_ly_thu_vien_phim.View.View_Login_Signup
{
    public partial class FormLogin : Form
    {
        private View_Main.FormLoginSignup frmLoginSignup;
        private User_model user;
        public FormLogin(FormLoginSignup frmLoginSignup)
        {
            InitializeComponent();
            this.frmLoginSignup = frmLoginSignup;
        }

        private void lbSignup_Click(object sender, EventArgs e)
        {
            frmLoginSignup.OpenChidForm(new View.View_Login_Signup.Formsignup(frmLoginSignup,user));
            Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           string username= txtUsername.Text;
           string password= txtPassword.Text;
           User_model user = new User_model();
           user.username = username;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                MessageBox.Show("Username và password không được để trống");
            else
            {
                User_controller userController = new User_controller();
                if (userController.CheckLoginUser(user, password)) 
                {
                    MessageBox.Show("Đăng nhập thành công");
                }
                else
                {
                    MessageBox.Show("Đăng nhập thất bại");
                }
            }
        }

        private void lbForgot_Click(object sender, EventArgs e)
        {
            frmLoginSignup.showForgot(user);
        }
    }

}
