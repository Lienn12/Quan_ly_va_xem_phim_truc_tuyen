using Quan_ly_thu_vien_phim.Model;
using System;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Drawing;


namespace Quan_ly_thu_vien_phim.View.View_useControl
{
    public partial class itemCacGoi : UserControl
    {
        private GoiDichVu_model goiDichVuModel;
        public event EventHandler<GoiDichVu_model> OnSelected;
        public itemCacGoi(GoiDichVu_model model)
        {
            InitializeComponent();
            goiDichVuModel = model;
            CacGoi.Padding = new Padding(20, 0, 0, 0);
            CacGoi.Text = goiDichVuModel.PlanName;
            lbPrice.Text = goiDichVuModel.Price.ToString("N0") + " VND";
            CacGoi.CheckedChanged += (s, e) =>
            {
                if (CacGoi.Checked)
                {
                    OnSelected?.Invoke(this, goiDichVuModel); 
                }
            };
        }
        public bool IsSelected()
        {
            return CacGoi.Checked;
        }
        public int GetSelectedPlanId()
        {
            return goiDichVuModel.PlanId;
        }
        private void CacGoi_CheckedChanged(object sender, EventArgs e)
        {
            if (CacGoi.Checked)
            {
                this.BackColor = Color.FromArgb(5, 38, 89);
                CacGoi.ForeColor = Color.White; 
                lbPrice.ForeColor = Color.White;
                foreach (Control control in this.Parent.Controls)
                {
                    if (control is itemCacGoi && control != this)
                    {
                        ((itemCacGoi)control).DeselectRadio();
                    }
                }
            }
            else
            {
                ResetColor();
            }
        }
        public void DeselectRadio()
        {
            CacGoi.Checked = false;
        }
        public void ResetColor()
        {
            this.BackColor = Color.White; 
            CacGoi.ForeColor = Color.Black; 
            lbPrice.ForeColor = Color.Black;
            CacGoi.Checked = false;
        }
    }
}
