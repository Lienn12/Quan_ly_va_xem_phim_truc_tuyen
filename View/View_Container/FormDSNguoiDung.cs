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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void FormDSNguoiDung_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;  // Tắt tính năng tạo dòng mới
                                                       // Nếu đã định nghĩa tiêu đề cột từ Designer, bỏ qua phần thêm cột này.

            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            // Thêm dữ liệu mẫu
            dataGridView1.Rows.Add(1, "Avengers123", "Nam", "10-10-2000", "aaa@gmail.com", "hakjhfasd");
            dataGridView1.Rows.Add(2, "Titanic", "Nam", "10-10-2000", "aaa@gmail.com", "hakjhfasd");
            dataGridView1.Rows.Add(3, "Inception", "Nam", "10-10-2000", "aaa@gmail.com", "hakjhfasd");
            //dataGridView1.Rows.Add(4, "Avatar", 2009);
            //dataGridView1.Rows.Add(5, "The Dark Knight", 2008);

            // Tùy chỉnh các thuộc tính (tuỳ chọn)
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Tự động căn chỉnh
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
