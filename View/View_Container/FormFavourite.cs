using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Main;
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

namespace Quan_ly_thu_vien_phim.View.View_Container
{
    public partial class FormFavourite : Form
    {
        private Favourite_model favourite;
        private Favourite_controller favouriteController= new Favourite_controller();
        private FormMainUser formMainUser;
        public FormFavourite(FormMainUser formMainUser)
        {
            this.formMainUser = formMainUser;
            InitializeComponent();
            this.Size = new Size(1000, 760);
            customizeDataGridView();
            this.favorite_Id.DataPropertyName = "FavouriteId";
            this.title.DataPropertyName = "Movie.Title";       
            this.Release_year.DataPropertyName = "Movie.Year";
        }
        private void customizeDataGridView()
        {
            dataGridView.EnableHeadersVisualStyles = false; 
            dataGridView.ColumnHeadersDefaultCellStyle.BackColor = Color.White; 
            dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(5, 38, 89);
            dataGridView.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 14, FontStyle.Bold);
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormFavourite_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rc = this.ClientRectangle;
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rc,
                Color.FromArgb(5, 38, 89),   
                Color.FromArgb(28, 181, 224), 
                LinearGradientMode.Vertical)) 
            {
                g.FillRectangle(brush, rc);
            }
        }
        public void ShowData()
        {
            dataGridView.Invalidate();
            try
            {   
                List<Favourite_model> favouriteList = favouriteController.GetFavorite(28);
                    dataGridView.Rows.Clear();
                    foreach (var review in favouriteList)
                    {
                        dataGridView.Rows.Add(
                            review.FavouriteId,      
                            review.Movie.Title,       
                            review.Movie.Year   
                        );
                    }
                MessageBox.Show("Dữ liệu đánh giá: " + favouriteList.Count.ToString());
                dataGridView.Refresh();
            }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
    }
}
