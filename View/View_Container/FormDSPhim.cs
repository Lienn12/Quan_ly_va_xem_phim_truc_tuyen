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
    public partial class FormDSPhim : Form
    {
        public FormDSPhim()
        {
            InitializeComponent();
        }

        private void FormDSPhim_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = false; // Tắt tự động tạo cột
            dataGridView1.Rows.Clear();
            dataGridView1.Rows.Add(1, "Inception", DateTime.Now.ToString("dd/MM/yyyy"));
            dataGridView1.Rows.Add(2, "Avatar", DateTime.Now.AddDays(-1).ToString("dd/MM/yyyy"));
            dataGridView1.Rows.Add(3, "Titanic", DateTime.Now.AddDays(-2).ToString("dd/MM/yyyy"));
        }
    }
}
