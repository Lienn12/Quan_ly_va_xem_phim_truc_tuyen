using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class TrangUser : Form
    {
        private FormMainUser formMainUser;
        private User_controller userController;
        private User_model user;
        public TrangUser(FormMainUser formMainUser)
        {
            this.formMainUser = formMainUser;
            InitializeComponent();
            this.Size = new Size(1000, 760);
            userController = new User_controller();
            user = new User_model();
            setKhoa(true);
        }
        private void setKhoa(bool a)
        {
            dateBirth.Enabled = !a;
            cbGioitinh.Enabled = !a;
            txtEmail.Enabled = !a;
        }
        private void TrangUser_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rc = this.ClientRectangle; 
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rc,
                Color.FromArgb(5, 38, 89),   
                Color.FromArgb(28, 181, 224), 
                LinearGradientMode.Vertical)) 
            {
                g.FillRectangle(brush, rc); 
            }
        }
        
        public void getInfor(User_model user)
        {
            if (user != null)
            {
                lbUserID.Text = user.userId.ToString();
                lbUsename.Text = user.username;

                dateBirth.Format = DateTimePickerFormat.Custom;
                dateBirth.CustomFormat = "dd/MM/yyyy";
                txtEmail.Text = user.email;
                if (user.birth != DateTime.MinValue)
                {
                    dateBirth.Value = user.birth;
                }
                else
                {
                    dateBirth.Value = DateTime.Now;  
                }
                if (!string.IsNullOrEmpty(user.gender) && cbGioitinh.Items.Contains(user.gender))
                {
                    cbGioitinh.SelectedItem = user.gender;  
                }
                else
                {
                    cbGioitinh.SelectedIndex = -1;  
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

        private void btnEdit_Click(object sender, EventArgs e)
        {
            setKhoa(false);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                User_model user = GetUserInfo();
                string username = lbUsename.Text;
                user.username = username;

                int id= int.Parse(lbUserID.Text);
                user.userId = id;

                bool isUpdated = userController.UpdateInfo(user);

                if (isUpdated)
                {
                    getInfor(user);
                    MessageBox.Show(this, "Đã sửa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    setKhoa(true);
                }
                else
                {
                    MessageBox.Show(this, "Không thể sửa thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(this, "Có lỗi khi kết nối cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"SQL Error: {ex.Message}");
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            getInfor(user);
        }
    }
}
