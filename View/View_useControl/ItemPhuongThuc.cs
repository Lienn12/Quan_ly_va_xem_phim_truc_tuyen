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

namespace Quan_ly_thu_vien_phim.View.View_useControl
{
    public partial class ItemPhuongThuc : UserControl
    {
        private PhuongThuc_model phuongThucModel;
        public event EventHandler<PhuongThuc_model> OnSelected;
        public ItemPhuongThuc(PhuongThuc_model model)
        {
            InitializeComponent();
            phuongThucModel = model;
            rPT.Padding = new Padding(20, 0, 0, 0);
            rPT.Text = phuongThucModel.MethodName;
            rPT.CheckedChanged += (s, e) =>
            {
                if (rPT.Checked)
                {
                    OnSelected?.Invoke(this, phuongThucModel);
                }
            };
        }
        public bool IsSelected()
        {
            return rPT.Checked;
        }
        public int GetSelectedMethodId()
        {
            return phuongThucModel.MethodId;
        }
        public void ResetColor()
        {
            this.BackColor = Color.White;
            rPT.ForeColor = Color.FromArgb(5, 38, 89);
        }

        private void rPT_CheckedChanged(object sender, EventArgs e)
        {
            if (rPT.Checked)
            {
                this.BackColor = Color.FromArgb(5, 38, 89);
                rPT.ForeColor = Color.White;
                foreach (Control control in this.Parent.Controls)
                {
                    if (control is ItemPhuongThuc && control != this)
                    {
                        ((ItemPhuongThuc)control).DeselectRadio();
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
            rPT.Checked = false;
        }

    }
}
