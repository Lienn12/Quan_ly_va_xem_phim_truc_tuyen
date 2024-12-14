using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Main;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
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
        private XemChiTietUser chiTiet;
        public FormFavourite(FormMainUser formMainUser)
        {
            this.formMainUser = formMainUser;
            InitializeComponent();
            this.Size = new Size(1000, 760);
            customizeDataGridView();
            ShowData();
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
            if (e.RowIndex >= 0) 
            {
                int movieId = GetMovieIdFromFavorite();
                int userId = GetUserIdFromFavorite();
                if (e.ColumnIndex == 3) 
                {
                    chiTiet = formMainUser.GetXemChiTiet();
                    chiTiet.showMovie(movieId, userId);
                    chiTiet.setShowBtnBackFavourite(true);
                    chiTiet.setShowBtnFavourite(false);
                    ShowData();
                    formMainUser.OpenChidForm(chiTiet, sender);
                }
            }
            else if (e.ColumnIndex == 4)
                {
                int favouriteid = Convert.ToInt32(dataGridView.Rows[e.RowIndex].Cells[0].Value);
                DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa phim này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            bool isDeleted = favouriteController.DeleteFavorite(favouriteid);
                            if (isDeleted)
                            {
                                MessageBox.Show("Xóa phim thành công!", "Thông báo");
                                ShowData(); 
                            }
                            else
                            {
                                MessageBox.Show("Xóa phim thất bại. Vui lòng kiểm tra lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi xóa phim: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
        }
        public int GetMovieIdFromFavorite()
        {
            int selectedRow = dataGridView.CurrentCell?.RowIndex ?? -1;
            if (selectedRow == -1)
            {
                return -1; 
            }
            if (dataGridView.Rows[selectedRow].Cells[0].Value == null ||
                !int.TryParse(dataGridView.Rows[selectedRow].Cells[0].Value.ToString(), out int favoriteID))
            {
                return -1; 
            }
            try
            {
                int movieID = favouriteController.GetMovieId(favoriteID);
                return movieID;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message); 
                return -1;
            }
        }

        public int GetUserIdFromFavorite()
        {
            int selectedRow = dataGridView.CurrentCell?.RowIndex ?? -1;
            if (selectedRow == -1)
            {
                return -1; 
            }

            if (dataGridView.Rows[selectedRow].Cells[0].Value == null ||
                !int.TryParse(dataGridView.Rows[selectedRow].Cells[0].Value.ToString(), out int favoriteID))
            {
                return -1; 
            }

            try
            {
                int userID = favouriteController.GetUserId(favoriteID);
                return userID;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message); 
                return -1;
            }
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
                dataGridView.Refresh();
            }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           
    }
}
