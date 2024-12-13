using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Main;
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
    public partial class PhimLoai : Form
    {
        private Movie_model movie = new Movie_model();
        private Movie_controller movieController = new Movie_controller();
        private TrangchuUser trangchuUser;
        private FormMainUser formMainUser;
        private Genre_model genre = new Genre_model();
        private Country_model country = new Country_model();
        private Format_model format = new Format_model();
        private Country_controller countryController = new Country_controller();
        private Genre_controller genreController = new Genre_controller();
        private Format_controller formatController = new Format_controller();
        public PhimLoai(TrangchuUser trangchuUser, FormMainUser formMainUser)
        {
            InitializeComponent();
            this.Size = new Size(999, 1100);
            this.formMainUser = formMainUser;
            this.trangchuUser = trangchuUser;
            LoadData();
        }
        public void Init(List<Movie_model> dsmovie)
        {
            try
            {
                if (dsmovie != null && dsmovie.Count > 0)
                {
                    pnlPhim.Controls.Clear();
                    foreach (var movie in dsmovie)
                    {
                        if (movie != null)
                        {
                            ItemPhimUser item = new ItemPhimUser(movie, formMainUser);
                            pnlPhim.Controls.Add(item);
                        }
                        else
                        {
                            MessageBox.Show("Movie_model is null.");
                        }
                    }
                    pnlPhim.Refresh();
                }
                else
                {
                    pnlPhim.Controls.Clear();
                    Label noMoviesLabel = new Label
                    {
                        Text = "Không có phim phù hợp!",
                        AutoSize = true,
                        Location = new Point(10, 10)
                    };
                    pnlPhim.Controls.Add(noMoviesLabel);
                    pnlPhim.Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadData()
        {
            try
            {
                List<Movie_model> dsmovie = movieController.GetDeXuat();
                Init(dsmovie);
                LoadDataComboGenres();
                LoadDataComboFormat();
                LoadDataComboCountry();
                SortComboBox();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi tải dữ liệu: " + ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        private void LoadDataComboBox<T>(ComboBox comboBox, List<T> dataList, string displayField)
        {
            try
            {
                comboBox.Items.Clear();

                if (dataList != null && dataList.Count > 0)
                {
                    foreach (T item in dataList)
                    {
                        var value = item.GetType().GetProperty(displayField)?.GetValue(item, null);
                        comboBox.Items.Add(value != null ? value.ToString() : item.ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Danh sách trống hoặc không hợp lệ");
                }

                comboBox.Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public void LoadDataComboGenres()
        {
            try
            {
                List<Genre_model> dsGenre = genreController.GetGenres();
                LoadDataComboBox(cbTheLoai, dsGenre, "GenreName");
                cbTheLoai.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadDataComboFormat()
        {
            try
            {
                List<Format_model> dsFormat = formatController.GetFormats();
                LoadDataComboBox(cbDinhDang, dsFormat, "FormatName");
                cbDinhDang.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadDataComboCountry()
        {
            try
            {
                List<Country_model> dsCountry = countryController.GetCountries();
                LoadDataComboBox(cbQuocGia, dsCountry, "CountrysName");
                cbQuocGia.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void SortComboBox()
        {
            string[] items = { "Sắp xếp", "Tên phim (A-Z)", "Năm phát hành", "Đánh giá" };
            cbSapXep.Items.Clear();

            foreach (string item in items)
            {
                cbSapXep.Items.Add(item);
            }

            cbSapXep.SelectedIndex = 0;
        }

        private int GetGenreIdFromModel(string genreName)
        {
            try
            {
                List<Genre_model> dsGenre = genreController.GetGenres();
                foreach (var genre in dsGenre)
                {
                    if (genre.GenreName.Equals(genreName))
                    {
                        return genre.GenreID;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        private int GetCountryIdFromModel(string countryName)
        {
            try
            {
                List<Country_model> dsCountry = countryController.GetCountries();
                foreach (var country in dsCountry)
                {
                    if (country.CountryName.Equals(countryName))
                    {
                        return country.CountryId;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }

        private int GetFormatIdFromModel(string formatName)
        {
            try
            {
                List<Format_model> dsFormat = formatController.GetFormats();
                foreach (var format in dsFormat)
                {
                    if (format.FormatName.Equals(formatName))
                    {
                        return format.FormatID;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return 0;
        }
        public void UpdateMoviesByGenre(Genre_model genre)
        {
            try
            {
                List<Movie_model> dsMovie = movieController.GetMoviesByGenreID(genre.GenreID);
                for (int i = 0; i < cbTheLoai.Items.Count; i++)
                {
                    string genreName = cbTheLoai.Items[i].ToString();
                    if (genreName.Equals(genre.GenreName))
                    {
                        cbTheLoai.SelectedIndex = i;
                        break;
                    }
                }
                Init(dsMovie);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateMoviesByCountry(Country_model country)
        {
            try
            {
                List<Movie_model> dsMovie = movieController.GetMoviesByCountryID(country.CountryId);
                for (int i = 0; i < cbQuocGia.Items.Count; i++)
                {
                    string countryName = cbQuocGia.Items[i].ToString();
                    if (countryName.Equals(country.CountryName))
                    {
                        cbQuocGia.SelectedIndex = i;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("After if countryName: " + countryName);
                    }
                }
                Init(dsMovie);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void UpdateMoviesByFormat(Format_model format)
        {
            try
            {
                if (format == null || string.IsNullOrEmpty(format.FormatName))
                {
                    MessageBox.Show("Format or format name cannot be null");
                }

                List<Movie_model> dsMovieBo = movieController.GetPhimBo();
                List<Movie_model> dsMovieLe = movieController.GetPhimLe();

                if (format.FormatName.Equals("Phim Bộ", StringComparison.OrdinalIgnoreCase))
                {
                    Init(dsMovieBo);
                }
                else if (format.FormatName.Equals("Phim Lẻ", StringComparison.OrdinalIgnoreCase))
                {
                    Init(dsMovieLe);
                }

                for (int i = 0; i < cbDinhDang.Items.Count; i++)
                {
                    string formatName = cbDinhDang.Items[i].ToString(); // Get the format name
                    if (formatName.Equals(format.FormatName, StringComparison.OrdinalIgnoreCase))
                    {
                        cbDinhDang.SelectedIndex = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedGenreName = cbTheLoai.SelectedItem.ToString();
                string selectedCountryName = cbQuocGia.SelectedItem.ToString();
                string selectedFormatName = cbDinhDang.SelectedItem.ToString();
                int selectedSort = cbSapXep.SelectedIndex;
                int selectedGenreId = GetGenreIdFromModel(selectedGenreName);
                int selectedCountryId = GetCountryIdFromModel(selectedCountryName);
                int selectedFormatId = GetFormatIdFromModel(selectedFormatName);
                List<Movie_model> dsMovie = movieController.SearchMovies(selectedGenreId, selectedCountryId, selectedFormatId, selectedSort);
                MessageBox.Show("selectedGenreId :" + selectedGenreId + " selectedCountryId : " + selectedCountryId + " selectedFormatId: " + selectedFormatId + " selectedSort:" + selectedSort);
                MessageBox.Show("dsMovie " + dsMovie.Count);
                Init(dsMovie);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PhimLoai_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rc = this.ClientRectangle;
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rc,
                Color.FromArgb(255, 255, 255),
                Color.FromArgb(28, 181, 224),
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(brush, rc);
            }
        }
    }
}
