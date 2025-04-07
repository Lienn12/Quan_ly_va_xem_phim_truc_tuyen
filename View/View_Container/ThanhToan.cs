using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Main;
using Quan_ly_thu_vien_phim.View.View_useControl;
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
    public partial class ThanhToan : Form
    {
        private GoiDichVu_controller goiController;
        private PhuongThuc_controller phuongThucController;
        private ThanhToan_controller thanhToanController;
        private FormMainUser FormMainUser;
        private ThanhToan_model thanhToanModel;
        private GoiDichVu_model selectedPlan = null;
        private PhuongThuc_model selectedMethod = null;

        public ThanhToan(FormMainUser formMainUser)
        {
            InitializeComponent();
            goiController = new GoiDichVu_controller();
            phuongThucController = new PhuongThuc_controller();
            thanhToanController= new ThanhToan_controller();
            ShowGoiDichVu();
            ShowPhuongThuc();
            FormMainUser = formMainUser;
            if (FormMainUser != null) 
            {
                User_model user = formMainUser.getUserModel();
                if (user != null)
                {
                    ShowThongTin(user.userId, user.username);
                }
            }
        }

        private void ShowGoiDichVu()
        {
            List<GoiDichVu_model> goiDichVus = goiController.GetPlans();
            panelDanhSachGoi.Controls.Clear();

            foreach (var plan in goiDichVus)
            {
                itemCacGoi item = new itemCacGoi(plan);
                item.Dock = DockStyle.Top;
                item.OnSelected += (sender, selectedGoi) =>
                {
                    selectedPlan = selectedGoi;
                    lbTenGoi.Text = selectedGoi.PlanName;
                    lbGia.Text = selectedGoi.Price.ToString("N0") + " VNĐ";
                    lbThoiHan.Text = selectedGoi.DurationDays + " tháng";
                    lbTong.Text = selectedGoi.Price.ToString("N0") + " VNĐ";
                };
                panelDanhSachGoi.Controls.Add(item);
            }
        }
        private void ShowPhuongThuc()
        {
            List<PhuongThuc_model> phuongThuc = phuongThucController.GetMethods();
            pnlPhuongThuc.Controls.Clear();

            foreach (var method in phuongThuc)
            {
                ItemPhuongThuc item = new ItemPhuongThuc(method);
                item.Dock = DockStyle.Top;
                item.OnSelected += (sender, selectedPhuongThuc) =>
                {
                    selectedMethod = selectedPhuongThuc;
                };
                pnlPhuongThuc.Controls.Add(item);
            }
        }

        public void ShowThongTin(int userID, string username)
        {
            lbUser.Text = username;
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (selectedPlan == null || selectedMethod == null)
            {
                MessageBox.Show("Vui lòng chọn gói dịch vụ và phương thức thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            User_model user = FormMainUser.getUserModel();
            if (user == null)
            {
                MessageBox.Show("Không thể xác định người dùng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int userId = user.userId;
            int planId = selectedPlan.PlanId;
            int methodId = selectedMethod.MethodId;
            decimal amount = (decimal)selectedPlan.Price;

            bool isSuccess = thanhToanController.CreateOrderByUserId(userId, planId, methodId, amount);

            if (isSuccess)
            {
                MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Thanh toán thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
