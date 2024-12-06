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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
