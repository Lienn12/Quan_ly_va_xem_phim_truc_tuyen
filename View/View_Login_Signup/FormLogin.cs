using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Container;
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
        private FormMain formMain = new FormMain();
        private FormMainUser formMainUser ;
        private View_Main.FormLoginSignup frmLoginSignup;
        private User_model user, userInfor;
        private Admin_model admin;
        private User_controller userController;
        private Admin_controller adminController;
        public FormLogin(FormLoginSignup frmLoginSignup)
        {
            InitializeComponent();
            this.frmLoginSignup = frmLoginSignup;
            userController= new User_controller();
            adminController = new Admin_controller();
            user = new User_model();
            admin = new Admin_model();
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
           user.username = username;
           admin.Username = username;
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lbErrorUser.Text = string.IsNullOrEmpty(username) ? "Username không được để trống" : "";
                lbErrorPass.Text = string.IsNullOrEmpty(password) ? "Password không được để trống" : "";
                return;
            }
                
            else
            {
                if (userController.CheckLoginUser(user, password)) 
                {
                    int userId = user.userId;
                    MessageBox.Show("Đăng nhập thành công");
                    //frmLoginSignup.Hide();
                    userInfor = userController.GetInfo(userId);
                    formMainUser = new FormMainUser();
                    formMainUser.setuserModel(user);
                    FormFavourite formFavourite = formMainUser.GetFavourite();
                    formFavourite.ShowData(userId);
                    TrangUser tranguser = formMainUser.getCaNhan();                   
                    tranguser.getInfor(userInfor);
                    ThanhToan thanhToan= formMainUser.getThanhToan();
                    thanhToan.ShowThongTin(userId, username);
                    formMainUser.ShowDialog();
                    frmLoginSignup.Close();
                }
                else if(adminController.CheckLoginAdmin(admin,password))
                {
                    MessageBox.Show("Đăng nhập thành công");
                    frmLoginSignup.Hide();
                    formMain.ShowDialog();
                    frmLoginSignup.Close();
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

        private void btnEyeHide_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '*')
            {
                btnEyeHide.BringToFront();
                txtPassword.PasswordChar = '\0';
                btnEyeShow.Visible = true;      
                btnEyeHide.Visible = false;
            }
        }
        private void btnEyeShow_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '\0')
            {
                btnEyeHide.BringToFront();
                txtPassword.PasswordChar = '*';
                btnEyeShow.Visible = false;    
                btnEyeHide.Visible = true;
            }
        }
    }

}
