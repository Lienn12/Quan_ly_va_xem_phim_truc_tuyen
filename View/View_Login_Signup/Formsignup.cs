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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Quan_ly_thu_vien_phim.View.View_Login_Signup
{
    public partial class Formsignup : Form
    {
        private View_Main.FormLoginSignup frmLoginSignup;
        private View_Login_Signup.VerifyCode VerifyCode;
        private User_controller userController;
        private User_model user;
        public Formsignup(FormLoginSignup frmLoginSignup, User_model user)
        {
            InitializeComponent();
            userController = new User_controller();
            this.frmLoginSignup = frmLoginSignup;
            this.user = user;
        }

        private void lbLogin_Click(object sender, EventArgs e)
        {
            frmLoginSignup.OpenChidForm(new View.View_Login_Signup.FormLogin(frmLoginSignup));
            Close();
        }
        public User_model getUserModel()
        {
            return this.user;
            if (user == null)
            {
                MessageBox.Show("user bi null");
            }
        }
        private void setNull()
        {
            txtUsename.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";
            txtConfirm.Text = "";
        }
        private bool validateInputs(User_model user, string password, string confirm)
        {
            bool valid = true;
            if (string.IsNullOrWhiteSpace(user.username))  // Check if the username is null or empty
            {
                txtUsename.Text = "Username không được để trống.";  // Display message in the corresponding TextBox
                valid = false;
            }
            else if (userController.CheckDuplicateUser(user.username))  // Check if the username already exists
            {
                txtUsename.Text = "Username đã tồn tại";  // Display message in the corresponding TextBox
                valid = false;
            }

            // Check if email is null or empty
            if (string.IsNullOrWhiteSpace(user.email))  // Check if the email is null or empty
            {
                txtEmail.Text = "Email không được để trống.";  // Display message in the corresponding TextBox
                valid = false;
            }
            else if (userController.CheckDuplicateEmail(user.email))  // Check if the email already exists
            {
                txtEmail.Text = "Email đã tồn tại.";  // Display message in the corresponding TextBox
                valid = false;
            }
            else if (!userController.CheckEmail(user.email))  // Check if the email is valid
            {
                txtEmail.Text = "Email không hợp lệ";  // Display message in the corresponding TextBox
                valid = false;
            }
 
            // Check if password is null or empty
            if (string.IsNullOrWhiteSpace(password))  // Check if the password is null or empty
            {
                txtPass.Text = "Password không được để trống.";  // Display message in the corresponding TextBox
                valid = false;
            }
            else if (!userController.CheckPassword(password))  // Check if the password is valid
            {
                txtPass.Text = "Mật khẩu không hợp lệ. Vui lòng nhập ít nhất 6 ký tự, bao gồm cả chữ, số và ký tự.";  // Display message in the corresponding TextBox
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
        private void btnSignup_Click(object sender, EventArgs e)
        {
            try
            {
                string username = txtUsename.Text;
                string email = txtEmail.Text;
                string pass = txtPass.Text;
                string confirm = txtConfirm.Text;
                user = new User_model(0, username, email, pass);
                bool valid = validateInputs(user, pass, confirm);
                if (valid)
                {
                    MessageEmail_controller ms = new MessageEmail_controller();
                    userController.CheckSignupUser(user, pass);
                    if (ms.sendEmail(user.email, user.verifyCode))
                    {
                        MessageBox.Show("Đã gửi code. Vui lòng kiểm tra Email!!","thong bao", MessageBoxButtons.OK,MessageBoxIcon.Information);
                        frmLoginSignup.ShowVerifyCode(user);                  
                    }
                    else
                    {
                        MessageBox.Show("gui email that bai");
                    }
                }
                    else
                {
                    MessageBox.Show("Thông tin đăng ký không hợp lệ. Vui lòng kiểm tra lại.");
                }
                }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("Đăng ký thất bại");
            }
        }
    }
}
