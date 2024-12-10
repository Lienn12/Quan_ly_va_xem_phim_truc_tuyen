using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class TrangUser : Form
    {
        private User_controller userController;
        private User_model user;
        public TrangUser()
        {
            InitializeComponent();
            this.Size = new Size(1000, 760);
            userController = new User_controller();
            user = new User_model();
        }

        private void TrangUser_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rc = this.ClientRectangle; // Kích thước của form

            // Tạo LinearGradientBrush từ #052659 đến #1CB5E0
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rc,
                Color.FromArgb(5, 38, 89),   // Màu bắt đầu (#052659)
                Color.FromArgb(28, 181, 224), // Màu kết thúc (#1CB5E0)
                LinearGradientMode.Vertical)) // Loang theo chiều dọc
            {
                g.FillRectangle(brush, rc); // Tô màu loang cho toàn bộ form
            }
        }
        public void setInfor(User_model user)
        {
            if (user != null)
            {
                lbUserID.Text = user.userId.ToString();
                lbUsename.Text = user.username;

                dateBirth.Format = DateTimePickerFormat.Custom;
                dateBirth.CustomFormat = "dd/MM/yyyy";

                // Kiểm tra giá trị birth trước khi gán cho DateTimePicker
                if (user.birth != DateTime.MinValue)
                {
                    dateBirth.Value = user.birth;
                }
                else
                {
                    dateBirth.Value = DateTime.Now;  // Gán giá trị mặc định nếu birth không hợp lệ
                }

                // Kiểm tra giá trị gender có null hay không trước khi gán
                if (!string.IsNullOrEmpty(user.gender) && cbGioitinh.Items.Contains(user.gender))
                {
                    cbGioitinh.SelectedItem = user.gender;  // Gán giới tính vào ComboBox
                }
                else
                {
                    cbGioitinh.SelectedIndex = -1;  // Nếu không có giới tính hợp lệ, bỏ chọn
                }
            }
        }



        public User_model GetUserInfo()
        {
            user.birth = dateBirth.Value != null ? dateBirth.Value : DateTime.Now;
            if (cbGioitinh.SelectedItem != null)
            {
                user.gender = cbGioitinh.SelectedItem.ToString();
            }
            else
            {
                user.gender = ""; 
            }
            user.email = txtEmail.Text;

            return user;
        }



    }
}
