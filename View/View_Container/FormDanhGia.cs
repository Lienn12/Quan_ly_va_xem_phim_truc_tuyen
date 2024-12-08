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
    public partial class FormDanhGia : Form
    {
        public FormDanhGia()
        {
            InitializeComponent();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormDanhGia_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false; // Tắt tự động tạo cột
            dataGridView1.Rows.Clear();
                dataGridView1.Rows.Add(1, "user1", "Inception", 4.5, DateTime.Now.ToString("dd/MM/yyyy"));
                dataGridView1.Rows.Add(2, "user2", "Avatar", 4.7, DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
                dataGridView1.Rows.Add(3, "user3", "Titanic", 5.0, DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy"));
           
        }
    }
}
