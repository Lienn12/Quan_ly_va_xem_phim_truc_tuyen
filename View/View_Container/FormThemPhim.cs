﻿using Quan_ly_thu_vien_phim.Controller;
using Quan_ly_thu_vien_phim.Model;
using Quan_ly_thu_vien_phim.View.View_Container;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Quan_ly_thu_vien_phim.View
{
    public partial class FormThemPhim : Form
    {
        private string filePath, videoPath;
        private Movie_controller movie_Controller = new Movie_controller();
        private Genre_controller genre_Controller = new Genre_controller();
        private Format_controller format_Controller = new Format_controller();
        private Country_controller country_Controller = new Country_controller();
        private FormMain formMain ;
        public FormThemPhim(FormMain formMain)
        {
            this.formMain = formMain;
            InitializeComponent();
            btnUpImg.Click -= btnUpImg_Click;
            btnUpImg.Click += btnUpImg_Click;

            btnUpVid.Click -= btnUpVid_Click;
            btnUpVid.Click += btnUpVid_Click;
            LoadDataComboGenres(cbType, genre_Controller.GetGenres());
            LoadDataComboFormats(cbFormat, format_Controller.GetFormats());
            LoadDataComboCountries(cbCountry, country_Controller.GetCountries());
            setNull();
        }

        private void FormThemPhim_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rc = this.ClientRectangle; // Kích thước của form

            // Tạo LinearGradientBrush từ #052659 đến #1CB5E0
            using (LinearGradientBrush brush = new LinearGradientBrush(
                rc,
                Color.FromArgb(5, 38, 89),   // Màu bắt đầu (#052659)
                Color.FromArgb(28, 181, 224), // Màu kết thúc (#1CB5E0)
                LinearGradientMode.Vertical)) // Loang theo chiều dọc
            {
                g.FillRectangle(brush, rc); // Tô màu loang cho toàn bộ form
            }
        }

        private void LoadDataComboBox<T>(ComboBox comboBox, List<T> dataList, string displayField)
        {
            try
            {
                comboBox.Items.Clear();
                if (dataList != null && dataList.Count > 0)
                {
                    foreach (var item in dataList)
                    {
                        if (item is Genre_model genreItem)
                        {
                            if (genreItem.GenreID == 1)
                                continue; // Skip items with GenreId == 1
                        }
                        else if (item is Format_model formatItem)
                        {
                            if (formatItem.FormatID == 1)
                                continue; // Skip items with FormatId == 1
                        }
                        else if (item is Country_model countryItem)
                        {
                            if (countryItem.CountryId == 1)
                                continue; // Skip items with CountryId == 1
                        }
                        comboBox.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Danh sách trống hoặc không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                comboBox.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadDataComboGenres(ComboBox comboBox, List<Genre_model> genreList)
        {
            LoadDataComboBox(comboBox, genreList, "GenreName"); // "GenreName" is the property for display
        }
        public void LoadDataComboFormats(ComboBox comboBox, List<Format_model> formatList)
        {
            LoadDataComboBox(comboBox, formatList, "FormatName"); // "FormatName" is the property for display
        }
        public void LoadDataComboCountries(ComboBox comboBox, List<Country_model> countryList)
        {
            LoadDataComboBox(comboBox, countryList, "CountryName"); // "CountryName" is the property for display
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            setNull();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtName.Text;
            int year = int.Parse(txtYear.Text);
            if (year < 0 || year > 2024)
            {
                MessageBox.Show("Vui lòng nhập năm hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string director = txtDirector.Text;
            string cast = txtCast.Text;
            Genre_model genre = (Genre_model)cbType.SelectedItem;
            int genreId = genre.GenreID;
            Country_model country = (Country_model)cbCountry.SelectedItem;
            int countryId = country.CountryId;
            Format_model format = (Format_model)cbFormat.SelectedItem;
            int formatId = format.FormatID;
            int episode = int.Parse(txtEpisode.Text);

            if (episode < 0)
            {
                MessageBox.Show("Số tập phải lớn hơn hoặc bằng 0!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string descrip = txtDescrip.Text;

            if (filePath == null || videoPath == null)
            {
                MessageBox.Show("Vui lòng chọn tệp hình ảnh và video!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            bool isVip = ckVip.Checked;
            if (movie_Controller.SaveInfo(title, year, director, cast, genreId, formatId, countryId, episode, descrip, filePath, videoPath, isVip))
            {
                MessageBox.Show("Đã thêm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                setNull();
            }
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            formMain.OpenChidForm(new View.View_Container.FormDSPhim(formMain),sender);
        }

        private void btnUpVid_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Video Files|*.mp4;*.avi;*.wmv;*.mov;*.mkv",
                Title = "Chọn video"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                videoPath = openFileDialog.FileName;
                if (!string.IsNullOrEmpty(videoPath) && File.Exists(videoPath))
                {
                    btnUpVid.BackColor = Color.Green; // Đổi màu nút thành màu xanh
                }
                else
                {
                    btnUpVid.BackColor = Color.LightSkyBlue; // Reset màu nếu không chọn file
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn file nào!");
            }
        }

        private void btnUpImg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Title = "Chọn ảnh để tải lên"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                {
                    try
                    {
                        using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                        {
                            var img = System.Drawing.Image.FromStream(stream);
                            Bitmap resizedImg = new Bitmap(img, pbMovie.Width, pbMovie.Height);
                            pbMovie.Image = resizedImg;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi tải ảnh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy tệp hình ảnh.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cbFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFormat.SelectedItem is Format_model selected)
            {
                string formatName = selected.FormatName;
                if (formatName == "Phim lẻ")
                {
                    txtEpisode.Text = "1";
                    txtEpisode.Enabled = false;
                }
                else if (formatName == "Phim bộ")
                {
                    txtEpisode.Enabled = true;
                    txtEpisode.Text = string.Empty;
                }
            }
        }

        public void setNull()
        {
            this.txtName.Text = string.Empty;
            this.txtYear.Text = string.Empty;
            this.txtDirector.Text = string.Empty;
            this.txtCast.Text = string.Empty;
            this.cbCountry.SelectedIndex = -1;
            this.cbFormat.SelectedIndex = -1;
            this.cbType.SelectedIndex = -1;
            this.txtEpisode.Text = string.Empty;
            this.txtDescrip.Text = string.Empty;
            this.pbMovie.Image = null; 
            this.filePath = null; 
            this.videoPath = null; 
            this.btnUpVid.BackColor = Color.LightSkyBlue;
            this.ckVip.Checked = false;
        }
    }
}
