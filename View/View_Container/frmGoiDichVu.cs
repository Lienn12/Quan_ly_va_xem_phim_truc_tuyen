using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
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
    public partial class frmGoiDichVu : Form
    {
        private GoiDichVu_controller goiDichVuController;

        public frmGoiDichVu()
        {
            InitializeComponent();
            
            SetupDataGridView();
            btnSua.Visible = false;
            goiDichVuController = new GoiDichVu_controller();
            ShowData();
        }
        private void SetupDataGridView()
        {
            dataGridView.RowTemplate.Height = 40;
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                row.Height = 40;
            }
           ((DataGridViewImageColumn)dataGridView.Columns[5]).ImageLayout = DataGridViewImageCellLayout.Normal;
            ((DataGridViewImageColumn)dataGridView.Columns[6]).ImageLayout = DataGridViewImageCellLayout.Zoom;
        }
        private void ShowData()
        {
            try
            {
                if (goiDichVuController == null)
                {
                    MessageBox.Show("Dữ liệu không có sẵn.");
                    return;
                }
                List<GoiDichVu_model> plans = goiDichVuController.GetPlans() ?? new List<GoiDichVu_model>();

                // Kiểm tra nếu plans là null
                if (plans == null)
                {
                    MessageBox.Show("Dữ liệu không có sẵn.");
                    return;
                }

                dataGridView.Rows.Clear();
                foreach (var plan in plans)
                {
                    dataGridView.Rows.Add(
                        plan.PlanId,
                        plan.PlanName,
                        plan.Price,
                        plan.DurationDays,
                        plan.Description
                    );
                }
                dataGridView.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (e.ColumnIndex == 5)
                {
                    int planId;
                    if (dataGridView.Rows[e.RowIndex].Cells[0].Value == null ||
                        !int.TryParse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out planId))
                    {
                        MessageBox.Show("Không tìm thấy ID gói dịch vụ, không thể sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string planName = dataGridView.Rows[e.RowIndex].Cells[1].Value?.ToString() ?? string.Empty;
                    decimal price;
                    if (dataGridView.Rows[e.RowIndex].Cells[2].Value != null &&
                        decimal.TryParse(dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString(), out price))
                    {
                        int durationDays;
                        if (dataGridView.Rows[e.RowIndex].Cells[3].Value != null &&
                            int.TryParse(dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString(), out durationDays))
                        {
                            string description = dataGridView.Rows[e.RowIndex].Cells[4].Value?.ToString() ?? string.Empty;

                            txtGoi.Text = planName;
                            txtGia.Text = price.ToString();
                            txtThoiGian.Text = durationDays.ToString();
                            txtMota.Text = description;
                            btnSua.Visible = true;
                            btnThem.Visible = false;
                            btnSua.Tag = planId;
                        }
                    }
                }
                else if (e.ColumnIndex == 6)
                {
                    int planId;
                    if (dataGridView.Rows[e.RowIndex].Cells[0].Value == null ||
                        !int.TryParse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out planId))
                    {
                        MessageBox.Show("Không tìm thấy ID gói dịch vụ, không thể xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa gói dịch vụ này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            bool isDeleted = goiDichVuController.DeletePlan(planId);
                            if (isDeleted)
                            {
                                ShowData();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi xóa gói dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string planName = txtGoi.Text;
            decimal price = decimal.Parse(txtGia.Text);
            int durationDays = int.Parse(txtThoiGian.Text);
            string description = txtMota.Text;

            if(string.IsNullOrEmpty(planName) || price <= 0 || durationDays <= 0 || string.IsNullOrEmpty(description))
{
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }


            GoiDichVu_model newPlan = new GoiDichVu_model(0, planName, price, durationDays, description); 
            Task.Run(() =>
            {
                bool result = goiDichVuController.AddPlan(newPlan);
                Invoke(new Action(() =>
                {
                    if (result)
                    {
                        MessageBox.Show("Thêm gói dịch vụ thành công!");
                        txtGia.Text = "";
                        txtGoi.Text = "";
                        txtMota.Text = "";
                        txtThoiGian.Text = "";
                        ShowData();
                    }
                    else
                    {
                        MessageBox.Show("Thêm gói dịch vụ thất bại.");
                    }
                }));
            });
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int planId = (int)btnSua.Tag;
            string planName = txtGoi.Text;
            decimal price = decimal.Parse(txtGia.Text);
            int durationDays = int.Parse(txtThoiGian.Text);
            string description = txtMota.Text;
            if (string.IsNullOrEmpty(planName) || price <= 0 || durationDays <= 0 || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            GoiDichVu_model updatedPlan = new GoiDichVu_model(planId, planName, price, durationDays, description); 
            bool isUpdated = goiDichVuController.UpdatePlan(updatedPlan);

            if (isUpdated)
            {
                MessageBox.Show("Cập nhật gói dịch vụ thành công!", "Thông báo");
                ShowData();
                btnSua.Visible = false;
                btnThem.Visible = true;
                txtGia.Text = "";
                txtGoi.Text = "";
                txtMota.Text = "";
                txtThoiGian.Text = "";
            }
            else
            {
                MessageBox.Show("Cập nhật gói dịch vụ thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
