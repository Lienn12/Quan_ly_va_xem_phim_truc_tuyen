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
using Vlc.DotNet.Forms;

namespace Quan_ly_thu_vien_phim.View
{
    public partial class FormThemPhim : Form
    {
        private string filePath, videoPath;
        public FormThemPhim()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Title = "Chọn ảnh để tải lên"
            };
            // Hiển thị hộp thoại chọn file
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn file
                filePath = openFileDialog.FileName;
                // Hiển thị ảnh trong PictureBox
                pictureBox1.Image = Image.FromFile(filePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                // Xử lý bổ sung (nếu cần)
                MessageBox.Show("Đã tải lên ảnh thành công!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog   // Tạo OpenFileDialog
            {
                Filter = "Video Files|*.mp4;*.avi;*.wmv;*.mov;*.mkv",
                Title = "Chọn video"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)   // Hiển thị hộp thoại chọn file
            {
                // Lấy đường dẫn file video được chọn
                videoPath = openFileDialog.FileName;
                // Hiển thị đường dẫn ra màn hình (MessageBox hoặc TextBox)
                MessageBox.Show("Đường dẫn video: " + videoPath);
                
            }
            else
            {
                // Người dùng bấm Cancel
                MessageBox.Show("Bạn chưa chọn file nào!");
            }
        }
    }
}
