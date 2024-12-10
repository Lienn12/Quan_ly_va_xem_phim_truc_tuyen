using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class FormDSNguoiDung : Form
    {
        public FormDSNguoiDung()
        {
            InitializeComponent();

            customizeDataGridView();
            //this.Size = new Size(881, 687);
            //panel1.Size = new Size(859, 818);
        }

        private void customizeDataGridView()
        {
            // Tùy chỉnh header DataGridView
            dataGridView1.EnableHeadersVisualStyles = false; // Tắt Visual Style
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White; // Màu nền header
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(84,131,179); // Màu chữ header
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9, FontStyle.Bold); // Phông chữ header
        }

        private void FormDSNguoiDung_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false; // Tắt tự động tạo cột
            // Lấy dữ liệu từ User_controller
            List<User_model> users = userController.GetUserData();
            if (users.Count > 0)
            {
                dataGridView1.DataSource = null; // Xóa dữ liệu cũ
                dataGridView1.DataSource = users; // Gán danh sách người dùng
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để hiển thị!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        private void SearchUsers(string keyword)
        {
            // Lấy danh sách người dùng từ controller
            User_controller userController = new User_controller();
            List<User_model> users = userController.GetUserData();  // Lấy tất cả dữ liệu ban đầu

            // Nếu từ khóa không rỗng, lọc danh sách
            if (!string.IsNullOrEmpty(keyword))
            {
                users = users.Where(user =>
                    user.userId.ToString().Contains(keyword) || // Tìm theo mã người dùng
                    user.username.ToLower().Contains(keyword.ToLower())  // Tìm theo tên người dùng
                ).ToList();
            }

            // Cập nhật DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = users;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            SearchUsers(txtSearch.Text);
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Tìm kiếm người dùng...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black; // Đổi màu chữ nếu cần
            }
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            User_controller userController = new User_controller();
            List<User_model> users = userController.GetUserData();
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                txtSearch.Text = "Tìm kiếm người dùng...";
                txtSearch.ForeColor = Color.Gray; // Đổi màu chữ khi lại có văn bản mặc định
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = users;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            // Nếu nhấn phím Enter, thực hiện tìm kiếm
            if (e.KeyCode == Keys.Enter)
            {
                string keyword = txtSearch.Text.Trim();
                if (!string.IsNullOrEmpty(keyword))
                {
                    // Thực hiện tìm kiếm
                    SearchUsers(keyword);
                }
                else
                {
                    // Nếu từ khóa tìm kiếm rỗng, hiển thị lại tất cả dữ liệu
                    SearchUsers("");  // Truyền một chuỗi rỗng để lấy lại toàn bộ dữ liệu
                }

                // Ngừng hành động mặc định của phím Enter (ngừng xuống dòng)
                e.Handled = true;
            }
        }
    }
}

