using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Main;
using Quan_ly_thu_vien_phim.View.View_useControl;
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
    public partial class FormPhimTrangChu : Form
    {
        private Movie_model movie= new Movie_model();
        private Movie_controller movieController = new Movie_controller();
        private FormTrangChu formTrangChu;
        private FormMain formMain;
        public FormPhimTrangChu(FormTrangChu formTrangChu, FormMain formMain)
        {
            InitializeComponent();
            this.formTrangChu = formTrangChu;
            this.formMain = formMain;
            loadDataPhimdx();
            loadDataPhimbo();
            loadDataPhimle();
            loadDataReview();
            
            this.Size = new Size(999, 1500);
        }
        private void loadDataPhimdx()
        {
            try
            {
                List<Movie_model> dsMovieDx = movieController.GetDeXuat();
                if (dsMovieDx != null && dsMovieDx.Count > 0)
                {
                    pnlPhimdx.Controls.Clear();
                    foreach (Movie_model movie in dsMovieDx)
                    {
                        ItemPhim item = new ItemPhim(movie, formMain);
                        pnlPhimdx.Controls.Add(item);
                    }
                    pnlPhimdx.Refresh();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadDataPhimbo()
        {
            try
            {
                List<Movie_model> dsMovieDx = movieController.GetPhimBo();
                if (dsMovieDx != null && dsMovieDx.Count > 0)
                {
                    pnlPhimbo.Controls.Clear();
                    foreach (Movie_model movie in dsMovieDx)
                    {
                        ItemPhim item = new ItemPhim(movie, formMain);
                        pnlPhimbo.Controls.Add(item);
                    }
                    pnlPhimbo.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadDataPhimle()
        {
            try
            {
                List<Movie_model> dsMovieDx = movieController.GetPhimLe();
                if (dsMovieDx != null && dsMovieDx.Count > 0)
                {
                    pnlPhimLe.Controls.Clear();
                    foreach (Movie_model movie in dsMovieDx)
                    {
                        ItemPhim item = new ItemPhim(movie, formMain);
                        pnlPhimLe.Controls.Add(item);
                    }
                    pnlPhimLe.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void loadDataReview()
        {
            try
            {
                List<Movie_model> dsMovieDx = movieController.GetReview();
                if (dsMovieDx != null && dsMovieDx.Count > 0)
                {
                    pnlReview.Controls.Clear();
                    foreach (Movie_model movie in dsMovieDx)
                    {
                        ItemReviewPhim item = new ItemReviewPhim(movie);
                        pnlReview.Controls.Add(item);
                    }
                    pnlReview.Refresh();
                }

                pnlReview.HorizontalScroll.Enabled = false;
                pnlReview.HorizontalScroll.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
