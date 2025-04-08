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
    public partial class FormGoiNguoiDung : Form
    {
        private UserVip_model userVip_Model;
        private UserVip_controller userVip_Controller;
        private FormMainUser FormMainUser;
        public FormGoiNguoiDung(FormMainUser FormMainUser)
        {
            InitializeComponent();
            userVip_Controller = new UserVip_controller();
            this.FormMainUser = FormMainUser;
            if (Session.CurrentUser != null)
            {
                showGoi(Session.CurrentUser.userId);
                bool result = userVip_Controller.IsUserVip(Session.CurrentUser.userId);
                if (!result)
                {
                    panel1.Visible = false;
                    btnHuy.Visible = false;
                    btnMuaGoi.Visible = true;
                }
                else
                {
                    btnMuaGoi.Visible = false;
                }
            }
            else
            {
                panel1.Visible = false;
                btnHuy.Visible = false;
                btnMuaGoi.Visible = true;
            }
            
        }

        public void showGoi(int userId)
        {
            UserVip_model userVip_Model = userVip_Controller.GetVipByUserId(userId);

            if (userVip_Model != null)
            {
                lbGoi.Text = userVip_Model.plan.PlanName;

                // Tính số ngày còn lại (nếu chưa hết hạn)
                TimeSpan thoiHan = userVip_Model.EndDate - DateTime.Now;
                lbThoiHan.Text = thoiHan.Days > 0 ? $"{thoiHan.Days} ngày" : "Hết hạn";

                // Hiển thị ngày bắt đầu và kết thúc
                lbstart.Text = userVip_Model.StartDate.ToString("dd/MM/yyyy");
                lbend.Text = userVip_Model.EndDate.ToString("dd/MM/yyyy");
            }
            else
            {
                panel1.Visible = false;
                btnHuy.Visible = false;
                btnMuaGoi.Visible = true;
            }
        }

        private void btnMuaGoi_Click(object sender, EventArgs e)
        {
            if (Session.CurrentUser == null)
            {
                MessageBox.Show("Bạn cần đăng nhập để mua gói dịch vụ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormLoginSignup formLoginSignup = new FormLoginSignup();
                //this.Hide();
                
                DialogResult result = formLoginSignup.ShowDialog();
                FormMainUser.Close();
            }
            else
            {
                FormMainUser.OpenChidForm(new View_Container.ThanhToan(FormMainUser), sender);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            if (userVip_Controller.CancelVip(Session.CurrentUser.userId))
            {
                MessageBox.Show("Hủy gói thành công!", "Thông báo");
                showGoi(Session.CurrentUser.userId);
            }
            else
            {
                MessageBox.Show("Không thể hủy gói hoặc không có gói hợp lệ.", "Lỗi");
            }
        }
    }
}
