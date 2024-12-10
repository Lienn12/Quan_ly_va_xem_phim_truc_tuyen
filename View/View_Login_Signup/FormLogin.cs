﻿using Quan_ly_thu_vien_phim.Controller;
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
                MessageBox.Show("Username và password không được để trống");
            else
            {
                if (userController.CheckLoginUser(user, password)) 
                {
                    MessageBox.Show("Đăng nhập thành công");
                }
                else if(adminController.CheckLoginAdmin(admin,password))
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
