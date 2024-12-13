using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quan_ly_thu_vien_phim.View.View_useControl
{
    public partial class Mota : UserControl
    {
        public Mota(String title, float star)
        {
            InitializeComponent();
            lbTitle.Text = title;
            lbRating.Text = star.ToString();
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            // Sử dụng Antialiasing để mượt mà
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Tạo màu với độ trong suốt (255,255,255,200)
            using (Brush brush = new SolidBrush(Color.FromArgb(200, 255, 255, 255)))
            {
                // Vẽ hình chữ nhật bo góc
                g.FillRectangle(brush, 0, 0, this.Width, this.Height);
            }
        }
    }
}
