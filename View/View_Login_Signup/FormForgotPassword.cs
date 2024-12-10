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
    public partial class FormForgotPassword : Form
    {
        private FormLoginSignup formLoginSignup;
        private User_model user;
        private User_controller userController;
        private MessageEmail_controller ms;
        public FormForgotPassword(FormLoginSignup formLoginSignup)
        {
            this.formLoginSignup = formLoginSignup;
            this.user = new User_model();
            userController =new User_controller();
            ms= new MessageEmail_controller();
            InitializeComponent();
        }
        public void setUserModel(User_model user)
        {
            this.user = user;
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            if (user == null)
            {
                user = new User_model(); // Khởi tạo nếu chưa tồn tại
            }
            user.email = email;
            if (user == null)
            {
                MessageBox.Show("user null");
            }
            if (string.IsNullOrEmpty(email))
            {
                txtEmail.Text = "Email không được để trống";
            }
            else if (!userController.CheckDuplicateEmail(email))
            {
                txtEmail.Text = "Email không tồn tại";
            }
            else
            {
                userController.ForgotPassword(user);
                ms.sendEmail(email, user.verifyCode);
            }
        }

        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (user == null || user.userId <= 0)
            {
                MessageBox.Show("Thông tin user không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string code = txtVerifyCode.Text;
            if (string.IsNullOrEmpty(code))
            {
                txtVerifyCode.Text = "Vui lòng nhập verify code";
            }
            else
            {
                if (user.verifyCode.Equals(code))
                {
                    userController.DoneVerify(user.userId);
                    MessageBox.Show("Xác minh thành công.");
                    formLoginSignup.showReset(user);
                }
                else
                {
                    MessageBox.Show("Mã xác minh sai.");
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            formLoginSignup.OpenChidForm(new View_Login_Signup.FormLogin(formLoginSignup));
        }
    }
}
