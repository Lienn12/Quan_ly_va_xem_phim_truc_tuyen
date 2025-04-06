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
    public partial class FormTrangChu : Form
    {
        private Form activeForm;
        private FormPhimloai formPhimLoai;
        private FormPhimTrangChu formPhimTrangChu;
        private FormMain formMain;
        private TableLayoutPanel genrePanel;
        private TableLayoutPanel countryPanel;
        private ContextMenuStrip genreMenu;
        private ContextMenuStrip countryMenu;
        private Genre_model genre = new Genre_model();
        private Country_model country = new Country_model();
        private Format_model format = new Format_model();
        private Movie_model movieModel=new Movie_model();
        private Movie_controller movieController = new Movie_controller();
        private Country_controller countryController= new Country_controller();
        private Genre_controller genreController= new Genre_controller();
        private Format_controller formatController = new Format_controller();
        public FormTrangChu(FormMain formMain)
        {
            this.formMain = formMain;
            InitializeComponent();
            formPhimLoai = new FormPhimloai(this,formMain);
            formPhimTrangChu = new FormPhimTrangChu(this, formMain);
            this.Size = new Size(999, 1800);
            initComboGenre();
            initComboCountry();
            InitPhim();
        }
        public void OpenChidForm(Form childForm, object sender)
        {
            if (this.activeForm != null)
            {
                this.activeForm.Hide();  // Thay vì đóng, bạn có thể ẩn form hiện tại
            }
            this.activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            this.pnlMain.Controls.Add(childForm);
            childForm.Show();
        }

        public void initComboGenre()
        {
            try
            {
                genrePanel = new TableLayoutPanel
                {
                    ColumnCount = 2,
                    AutoSize = true,
                    BackColor = Color.FromArgb(5, 38, 89),
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                    Width = 250,
                    GrowStyle = TableLayoutPanelGrowStyle.AddRows,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(5, 2, 5, 2)
                };
                genrePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
                genrePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

                List<Genre_model> dsGenre = genreController.GetGenres();
                int row = 0;
                int col = 0;

                foreach (var genre in dsGenre)
                {
                    if (genre.GenreID == 1)
                        continue;

                    Label lb = new Label
                    {
                        Text = genre.GenreName,
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.White,
                        AutoSize = false ,
                        Width = 200,
                        Height = 30,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Cursor = Cursors.Hand
                    };
                    genrePanel.Controls.Add(lb, col, row);

                    lb.MouseClick += (sender, e) =>
                    {
                        lb.ForeColor = Color.FromArgb(125, 160, 202);
                        openGenre(genre, sender);
                        genreMenu.Hide();
                    };

                    lb.MouseEnter += (sender, e) => lb.ForeColor = Color.FromArgb(125, 160, 202);
                    lb.MouseLeave += (sender, e) => lb.ForeColor = Color.White;
                    col++;
                    if (col >= 2)
                    {
                        col = 0;
                        row++;
                    }
                }

                genreMenu = new ContextMenuStrip();
                genreMenu.Items.Add(new ToolStripControlHost(genrePanel));
                lbTheLoai.MouseEnter += (sender, e) =>
                {
                    lbTheLoai.ForeColor = Color.FromArgb(192, 232, 255);
                    genreMenu.Show(lbTheLoai, 0, lbTheLoai.Height);
                };

                lbTheLoai.MouseLeave += (sender, e) => lbTheLoai.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void initComboCountry()
        {
            try
            {
                 countryPanel = new TableLayoutPanel
                {
                    ColumnCount = 2,
                    AutoSize = true,
                    BackColor = Color.FromArgb(5, 38, 89),
                    CellBorderStyle = TableLayoutPanelCellBorderStyle.None,
                    GrowStyle = TableLayoutPanelGrowStyle.AddRows,
                    Padding = new Padding(0),
                     Margin = new Padding(0)
                 };
                countryPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
                countryPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

                List<Country_model> dsCountry = countryController.GetCountries();
                int row = 0;
                int col = 0;

                foreach (var country in dsCountry)
                {
                    if (country.CountryId == 1)
                        continue;

                    Label lb = new Label
                    {
                        Text = country.CountryName,
                        Font = new Font("Segoe UI", 12),
                        ForeColor = Color.White,
                        AutoSize = false,
                        Width = 300,
                        Height = 30,
                        TextAlign = ContentAlignment.MiddleLeft,
                        Cursor = Cursors.Hand
                    };
                    countryPanel.Controls.Add(lb, col, row);

                    lb.MouseClick += (sender, e) =>
                    {
                        lb.ForeColor = Color.FromArgb(125, 160, 202);
                        openCountry(country, sender);
                        countryMenu.Hide();
                    };

                    lb.MouseEnter += (sender, e) => lb.ForeColor = Color.FromArgb(125, 160, 202);
                    lb.MouseLeave += (sender, e) => lb.ForeColor = Color.White;
                    col++;
                    if (col >= 2)
                    {
                        col = 0;
                        row++;
                    }
                }

                countryMenu = new ContextMenuStrip();
                countryMenu.Items.Add(new ToolStripControlHost(countryPanel));
                btnQuocGia.MouseEnter += (sender, e) =>
                {
                    btnQuocGia.ForeColor = Color.FromArgb(192, 232, 255);
                    countryMenu.Show(btnQuocGia, 0, btnQuocGia.Height);
                };

                btnQuocGia.MouseLeave += (sender, e) => btnQuocGia.ForeColor = Color.White;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void InitPhim()
        {
            btnPhimMoi.MouseClick += (s, e) =>
            {
                openPhimmoi(movieModel,s); 
                genreMenu.Visible = false; 
            };
            btnPhimBo.MouseClick += (s, e) =>
            {
                format.FormatName = "Phim Bộ"; 
                openFormat(format,s); 
                genreMenu.Visible = false; 
            };
            btnPhimLe.MouseClick += (s, e) =>
            {
                format.FormatName = "Phim Lẻ";
                openFormat(format,s); 
                genreMenu.Visible = false;
            };
 
        }

        private void openGenre(Genre_model genre, object sender)
        {
            formPhimLoai.UpdateMoviesByGenre(genre);
            OpenChidForm(formPhimLoai, sender);
        }

        private void openCountry(Country_model country, object sender)
        {
            formPhimLoai.UpdateMoviesByCountry(country);
            OpenChidForm(formPhimLoai, sender);
        }

        private void openPhimmoi(Movie_model movieModel, object sender)
        {
            formPhimLoai.LoadData();
            OpenChidForm(formPhimLoai, sender);
        }

        private void openFormat(Format_model format, object sender)
        {
            formPhimLoai.UpdateMoviesByFormat(format);
            OpenChidForm(formPhimLoai, sender);
        }

        private void FormTrangChu_Load(object sender, EventArgs e)
        {
            OpenChidForm(formPhimTrangChu, sender);
        }
    }
}
