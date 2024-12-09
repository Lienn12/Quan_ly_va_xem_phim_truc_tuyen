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
        public FormLogin(FormLoginSignup frmLoginSignup)
        {
            InitializeComponent();
            this.frmLoginSignup = frmLoginSignup;
        }

        private void lbSignup_Click(object sender, EventArgs e)
        {
            frmLoginSignup.OpenChidForm(new View.View_Login_Signup.Formsignup(frmLoginSignup));
            Close();
        }

        //private void getUserData(object sender, EventArgs e)
        //{
       
        //}
        private void showUserData(List<User_model> users)
        { 
            if(users.Count > 0)
            {
                User_model user = users[0];
                txtUsername.Text= user.username;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            User_controller user_Controller = new User_controller();
            List<User_model> users = user_Controller.getUserData();
            showUserData(users);

        }
    }

}
