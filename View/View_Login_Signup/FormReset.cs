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
    public partial class FormReset : Form
    {
        private FormLoginSignup formLoginSignup;
        private User_model user;
        private User_controller userController;
        public FormReset(FormLoginSignup formLoginSignup)
        {
            InitializeComponent();
            userController=new User_controller();
            this.formLoginSignup = formLoginSignup;
        }
        public void setUserModel(User_model user)
        {
            this.user = user;
        }
        private bool validateInputs(String password, String confirm)
        {
            bool valid = true;
            //ktr password
            // Check if password is null or empty
            if (string.IsNullOrWhiteSpace(password))  // Check if the password is null or empty
            {
                txtNew.Text = "Password không được để trống.";  // Display message in the corresponding TextBox
                valid = false;
            }
            else if (!userController.CheckPassword(password))  // Check if the password is valid
            {
                txtNew.Text = "Mật khẩu không hợp lệ. Vui lòng nhập ít nhất 6 ký tự, bao gồm cả chữ, số và ký tự.";  // Display message in the corresponding TextBox
                valid = false;
            }

            // Check if confirm password is null or empty
            if (string.IsNullOrWhiteSpace(confirm))  // Check if confirm password is null or empty
            {
                txtConfirm.Text = "Xác nhận mật khẩu không được để trống.";  // Display message in the corresponding TextBox
                valid = false;
            }
            else if (password != confirm)  // Check if confirm password matches the password
            {
                txtConfirm.Text = "Xác nhận mật khẩu không khớp. Vui lòng nhập lại";  // Display message in the corresponding TextBox
                valid = false;
            }
            return valid;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            string newPass = txtNew.Text;
            string confirm = txtConfirm.Text;
            bool valid = validateInputs(confirm, newPass);
            if (valid)
            {
                userController.ResetPassword(user.email,newPass);
                MessageBox.Show("Reset password thành công");
                formLoginSignup.OpenChidForm(new View.View_Login_Signup.FormLogin(formLoginSignup));
            }
            else
            {
                MessageBox.Show("Reset thất bại!!", "loi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            formLoginSignup.showForgot(user);
        }
    }
}
