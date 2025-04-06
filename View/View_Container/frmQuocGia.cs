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
    public partial class frmQuocGia : Form
    {
        private Country_controller countryController;

        public frmQuocGia()
        {
            InitializeComponent();
            countryController = new Country_controller();
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
            ((DataGridViewImageColumn)dataGridView.Columns[3]).ImageLayout = DataGridViewImageCellLayout.Normal;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string countryName = txtTenQuocGia.Text; 

            if (string.IsNullOrEmpty(countryName))
            {
                MessageBox.Show("Vui lòng nhập tên quốc gia.");
                return;
            }
            Country_model newCountry = new Country_model(0, countryName);
            bool result = countryController.AddCountry(newCountry);
            if (result)
            {
                MessageBox.Show("Thêm quốc gia thành công!");
                ShowData(); // Hiển thị lại dữ liệu sau khi thêm
            }
            else
            {
                MessageBox.Show("Thêm quốc gia thất bại.");
            }

        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 2)
                {
                    int countryId;
                    if (dataGridView.Rows[e.RowIndex].Cells[0].Value == null ||
                        !int.TryParse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out countryId))
                    {
                        MessageBox.Show("Không tìm thấy ID quốc gia, không thể sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string countryName = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();

                    txtTenQuocGia.Text = countryName;
                    btnSua.Visible = true;
                    btnSua.Tag = countryId;
                }
                else if (e.ColumnIndex == 3)
                {
                    int countryId;
                    if (dataGridView.Rows[e.RowIndex].Cells[0].Value == null ||
                        !int.TryParse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out countryId))
                    {
                        MessageBox.Show("Không tìm thấy ID quốc gia, không thể xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa quốc gia này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            bool isDeleted = countryController.DeleteCountry(countryId);
                            if (isDeleted)
                            {
                                ShowData(); 
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi xóa quốc gia: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void btnSua_Click(object sender, EventArgs e)
        {

            int countryId = (int)btnSua.Tag;  
            string newCountryName = txtTenQuocGia.Text;

            // Kiểm tra nếu tên quốc gia rỗng
            if (string.IsNullOrEmpty(newCountryName))
            {
                MessageBox.Show("Vui lòng nhập tên quốc gia.");
                return;
            }

            // Gọi phương thức từ controller để cập nhật thông tin quốc gia
            Country_model updatedCountry = new Country_model(countryId, newCountryName);
            bool isUpdated = countryController.UpdateCountry(updatedCountry);

            if (isUpdated)
            {
                MessageBox.Show("Cập nhật quốc gia thành công!", "Thông báo");
                ShowData(); 
                btnSua.Visible = false;
                txtTenQuocGia.Text = " ";
            }
            else
            {
                MessageBox.Show("Cập nhật quốc gia thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void ShowData()
        {
            try
            {
                List<Country_model> CountryList = countryController.GetCountries();
                dataGridView.Rows.Clear();
                int index = 1;
                foreach (var country in CountryList)
                {
                    if (country.CountryId == 1)
                    {
                        continue; 
                    }
                    dataGridView.Rows.Add(
                        country.CountryId, 
                        country.CountryName
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
