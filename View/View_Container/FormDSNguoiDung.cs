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
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(1, "user1", "Inception", 4.5, DateTime.Now.ToString("dd/MM/yyyy"));
            dataGridView1.Rows.Add(2, "user2", "Avatar", 4.7, DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
            dataGridView1.Rows.Add(3, "user3", "Titanic", 5.0, DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy"));
        }
    }
}
