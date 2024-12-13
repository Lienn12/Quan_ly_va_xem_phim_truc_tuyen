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
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace Quan_ly_thu_vien_phim.View.View_Login_Signup
{
    public partial class VerifyCode : Form
    {
        private FormLoginSignup formLoginSignup;
        private User_controller userController;
        private User_model user;

        // Constructor nhận thông tin từ FormLoginSignup
        public VerifyCode(FormLoginSignup formLoginSignup, User_model user)
        {
            InitializeComponent();
            this.formLoginSignup = formLoginSignup;
            userController = new User_controller();
            this.user = user;
        }
        public void setUserModel(User_model user)
        {
            this.user = user;  
        }
        private void btnOK_Click(object sender, EventArgs e)
        {   
            try
            {
                if (user == null)
                {
                    MessageBox.Show("User object is not initialized.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
                        }
                if (userController.VerifyCodeWithUser(user.userId, txtCode.Text))
                {
                    userController.DoneVerify(user.userId);
                    MessageBox.Show("Verify Code chính xác");
                    formLoginSignup.OpenChidForm(new View.View_Login_Signup.FormLogin(formLoginSignup));
                    Close();
                }
                else
                {
                    MessageBox.Show("Khong chính xác");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("loi: "+ ex.Message);
            }
            
        }

        private void btnCancel_Click_1(object sender, EventArgs e)
        {
            Close();
        }
    }
}
