using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Main;
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
    public partial class frmTheLoai : Form
    {
        private Genre_controller categoryController;  // Đổi tên controller

        public frmTheLoai()
        {
            InitializeComponent();
            categoryController = new Genre_controller();  // Đổi tên controller
            ShowData();
            SetupDataGridView();
            btnSua.Visible = false;
        }
        private void SetupDataGridView()
        {
            dataGridView.RowTemplate.Height = 40;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.Height = 40;
            }
           ((DataGridViewImageColumn)dataGridView.Columns[2]).ImageLayout = DataGridViewImageCellLayout.Normal;
            ((DataGridViewImageColumn)dataGridView.Columns[3]).ImageLayout = DataGridViewImageCellLayout.Zoom;

        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            string categoryName = txtTenTheLoai.Text;

            if (string.IsNullOrEmpty(categoryName))
            {
                MessageBox.Show("Vui lòng nhập tên thể loại.");
                return;
            }
            Genre_model newCategory = new Genre_model(0, categoryName);  
            bool result = categoryController.AddGenre(newCategory); 
            if (result)
            {
                MessageBox.Show("Thêm thể loại thành công!");
                ShowData(); // Hiển thị lại dữ liệu sau khi thêm
            }
            else
            {
                MessageBox.Show("Thêm thể loại thất bại.");
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)  
                {
                    int categoryId;
                    if (dataGridView.Rows[e.RowIndex].Cells[0].Value == null ||
                        !int.TryParse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out categoryId))
                    {
                        MessageBox.Show("Không tìm thấy ID thể loại, không thể sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string genreName = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();

                    txtTenTheLoai.Text = genreName;  
                    btnSua.Visible = true;
                    btnSua.Tag = categoryId;
                }
                else if (e.ColumnIndex == 3)  
                {
                    int categoryId;
                    if (dataGridView.Rows[e.RowIndex].Cells[0].Value == null ||
                        !int.TryParse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out categoryId))
                    {
                        MessageBox.Show("Không tìm thấy ID thể loại, không thể xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa thể loại này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            bool isDeleted = categoryController.DeleteGenre(categoryId);  
                            if (isDeleted)
                            {
                                ShowData();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi xóa thể loại: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int categoryId = (int)btnSua.Tag;
            string newCategoryName = txtTenTheLoai.Text; 
            if (string.IsNullOrEmpty(newCategoryName))
            {
                MessageBox.Show("Vui lòng nhập tên thể loại.");
                return;
            }

            Genre_model updatedCategory = new Genre_model(categoryId, newCategoryName);  
            bool isUpdated = categoryController.UpdateGenre(updatedCategory); 

            if (isUpdated)
            {
                MessageBox.Show("Cập nhật thể loại thành công!", "Thông báo");
                ShowData();
                btnSua.Visible = false;
                txtTenTheLoai.Text = " ";
            }
            else
            {
                MessageBox.Show("Cập nhật thể loại thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowData()
        {
            try
            {
                List<Genre_model> CategoryList = categoryController.GetGenres();  
                dataGridView.Rows.Clear();
                int index = 1;
                foreach (var Genre in CategoryList)
                {
                    if (Genre.GenreID == 1) 
                    {
                        continue;
                    }
                    dataGridView.Rows.Add(
                        Genre.GenreID,  
                        Genre.GenreName
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
