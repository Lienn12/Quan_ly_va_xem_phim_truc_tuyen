using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class ThemTap : Form
    {
        private int idPhim;
        private Tap_model episode = new Tap_model();
        private Episode_controller episode_Controller = new Episode_controller();
        public ThemTap(int soTap, int movieId, string tenPhim)
        {
            InitializeComponent();
            idPhim = movieId;
            lblTenPhim.Text = tenPhim;
            this.Load += ThemTap_Load;
            for (int i = 1; i <= soTap; i++)
            {
                dataGridView1.Rows.Add("Tập "+i, "", Properties.Resources.upvidicon, Properties.Resources.removeicon);
                dataGridView1.CellContentClick -= dataGridView1_CellContentClick; // Xóa sự kiện cũ nếu có
                dataGridView1.CellContentClick += dataGridView1_CellContentClick; // Gán sự kiện mới
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            // Cột "Thêm" (Column3)
            if (e.ColumnIndex == dataGridView1.Columns["Column3"].Index)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.DefaultCellStyle.BackColor = Color.White; // Reset về màu trắng
                }
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightBlue; // Màu khi click
                // Kiểm tra nếu nút "Thêm" đã bị khóa (tránh mở lại hộp thoại không cần thiết)
                var currentCellValue = dataGridView1.Rows[e.RowIndex].Cells["Column2"].Value?.ToString();
                if (!string.IsNullOrEmpty(currentCellValue)) return;
                // Mở hộp thoại chọn file
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Filter = "Video Files|*.mp4;*.mkv;*.avi|All Files|*.*";
                    if (ofd.ShowDialog() == DialogResult.OK)
                    {
                        // Gán link vào cột "Link"
                        dataGridView1.Rows[e.RowIndex].Cells["Column2"].Value = ofd.FileName;

                        // Khóa nút "Thêm" bằng cách thay đổi icon
                        dataGridView1.Rows[e.RowIndex].Cells["Column3"].Value = Properties.Resources._lock;
                        dataGridView1.Rows[e.RowIndex].Cells["Column3"].ReadOnly = true;
                    }
                }
            }
            // Cột "Xóa" (Column4)
            else if (e.ColumnIndex == dataGridView1.Columns["Column4"].Index)
            {
                dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
                // Xóa link video
                dataGridView1.Rows[e.RowIndex].Cells["Column2"].Value = "";

                // Mở khóa nút "Thêm" bằng cách thay đổi icon
                dataGridView1.Rows[e.RowIndex].Cells["Column3"].Value = Properties.Resources.upvidicon;
                dataGridView1.Rows[e.RowIndex].Cells["Column3"].ReadOnly = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool isSuccess = true;

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                // Bỏ qua hàng mới hoặc hàng không hợp lệ
                if (row.IsNewRow) continue;

                // Lấy giá trị từ cột "Tên tập" và "Link video"
                string episodeName = row.Cells["Column1"].Value?.ToString(); // Cột tên tập (Column1)
                string videoPath = row.Cells["Column2"].Value?.ToString();  // Cột đường dẫn video (Column2)

                // Kiểm tra dữ liệu có hợp lệ không
                if (string.IsNullOrWhiteSpace(episodeName) || string.IsNullOrWhiteSpace(videoPath))
                {
                    MessageBox.Show($"Hàng chứa tên tập '{episodeName}' không hợp lệ! Hãy điền đầy đủ thông tin.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isSuccess = false;
                    break;
                }

                // Gọi hàm SaveTap để lưu thông tin tập vào cơ sở dữ liệu
                episode_Controller.SaveTap(episodeName, idPhim, videoPath);
                isSuccess = true;
            }

            // Thông báo trạng thái lưu sau khi kết thúc vòng lặp
            if (isSuccess)
            {
                MessageBox.Show("Tất cả các tập đã được lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Một số tập không thể lưu. Vui lòng kiểm tra lại dữ liệu!","Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ThemTap_Load(object sender, EventArgs e)
        {
            List<Tap_model> episodes = episode_Controller.GetEpisodesByMovieID(idPhim);

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;

                // Lấy tên tập từ cột "Tên tập" (Column1)
                string currentEpName = row.Cells["Column1"].Value?.ToString();

                // Tìm tập trong danh sách lấy từ cơ sở dữ liệu
                var matchedEpisode = episodes.FirstOrDefault(episode => episode.epName == currentEpName);
                if (matchedEpisode != null)
                {
                    // Cập nhật đường dẫn video vào cột "Link video" (Column2)
                    row.Cells["Column2"].Value = matchedEpisode.vidPathTap;

                    // Xóa tập đã xử lý khỏi danh sách để tránh thêm lặp lại
                    episodes.Remove(matchedEpisode);
                }
            }

            // Thêm các tập phim vào DataGridView
            foreach (var episode in episodes)
            {
                dataGridView1.Rows.Add(episode.epName, episode.vidPathTap);
            }
        }

        private void ThemTap_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //Rectangle rc = this.ClientRectangle; // Kích thước của form

            //// Tạo LinearGradientBrush từ #052659 đến #1CB5E0
            //using (LinearGradientBrush brush = new LinearGradientBrush(
            //    rc,
            //    Color.FromArgb(5, 38, 89),   // Màu bắt đầu (#052659)
            //    Color.FromArgb(28, 181, 224), // Màu kết thúc (#1CB5E0)
            //    LinearGradientMode.Vertical)) // Loang theo chiều dọc
            //{
            //    g.FillRectangle(brush, rc); // Tô màu loang cho toàn bộ form
            //}
        }
    }
}
