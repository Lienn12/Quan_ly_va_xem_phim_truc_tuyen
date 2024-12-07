using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View
{
    public partial class FormThemPhim : Form
    {
        private string filePath, videoPath;
        private Form activeForm;
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
                pbMovie.Image = Image.FromFile(filePath);
                pbMovie.SizeMode = PictureBoxSizeMode.Zoom;
                // Xử lý bổ sung (nếu cần)
                MessageBox.Show("Đã tải lên ảnh thành công!");
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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
