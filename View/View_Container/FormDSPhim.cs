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
      
            
            dataGridView1.AllowUserToAddRows = false;  // Tắt tính năng tạo dòng mới
            // Nếu đã định nghĩa tiêu đề cột từ Designer, bỏ qua phần thêm cột này.
            
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Blue;
            // Thêm dữ liệu mẫu
            dataGridView1.Rows.Add(1, "Avengers: Endgame", 2019);
            dataGridView1.Rows.Add(2, "Titanic", 1997);
            dataGridView1.Rows.Add(3, "Inception", 2010);
            //dataGridView1.Rows.Add(4, "Avatar", 2009);
            //dataGridView1.Rows.Add(5, "The Dark Knight", 2008);

            // Tùy chỉnh các thuộc tính (tuỳ chọn)
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // Tự động căn chỉnh
        }

    }
}
