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
    public partial class formPhuongThuc : Form
    {
        private PhuongThuc_controller phuongThucController;
        public formPhuongThuc()
        {
            InitializeComponent();
            phuongThucController = new PhuongThuc_controller();
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
                        MessageBox.Show("Không tìm thấy ID phương thức, không thể sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    string genreName = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();

                    txtPhuongThuc.Text = genreName;
                    btnSua.Visible = true;
                    btnSua.Tag = categoryId;
                }
                else if (e.ColumnIndex == 3)
                {
                    int methodId;
                    if (dataGridView.Rows[e.RowIndex].Cells[0].Value == null ||
                        !int.TryParse(dataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out methodId))
                    {
                        MessageBox.Show("Không tìm thấy ID phương thức, không thể xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa phương thức này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            bool isDeleted = phuongThucController.DeleteMethod(methodId);
                            if (isDeleted)
                            {
                                ShowData();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi xóaphương thức: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
        }
        private void btnthem_Click(object sender, EventArgs e)
        {
            string methodName = txtPhuongThuc.Text;
            if (string.IsNullOrEmpty(methodName))
            {
                MessageBox.Show("Vui lòng nhập tên phương thức.");
                return;
            }

            PhuongThuc_model newMethod = new PhuongThuc_model(0, methodName);
            Task.Run(() =>
            {
                bool result = phuongThucController.AddMethod(newMethod);
                Invoke(new Action(() =>
                {
                    if (result)
                    {
                        MessageBox.Show("Thêm phương thức thành công!");
                        ShowData();
                    }
                    else
                    {
                        MessageBox.Show("Thêm phương thức thất bại.");
                    }
                }));
            });
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int methodId = (int)btnSua.Tag;
            string newMethodName = txtPhuongThuc.Text;
            if (string.IsNullOrEmpty(newMethodName))
            {
                MessageBox.Show("Vui lòng nhập tênphương thức.");
                return;
            }

            PhuongThuc_model updatedMethod = new PhuongThuc_model(methodId, newMethodName);
            bool isUpdated = phuongThucController.UpdateMethod(updatedMethod);

            if (isUpdated)
            {
                MessageBox.Show("Cập nhật phương thức thành công!", "Thông báo");
                ShowData();
                btnSua.Visible = false;
                txtPhuongThuc.Text = " ";
            }
            else
            {
                MessageBox.Show("Cập nhật phương thức thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ShowData()
        {
            try
            {
                List<PhuongThuc_model> phuongThucs = phuongThucController.GetMethods();
                dataGridView.Rows.Clear();
                foreach (var method in phuongThucs)
                {
                    dataGridView.Rows.Add(
                        method.MethodId,
                        method.MethodName
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
