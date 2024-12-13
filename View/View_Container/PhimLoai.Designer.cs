namespace Quan_ly_thu_vien_phim.View.View_Container
{
    partial class PhimLoai
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlPhim = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cbSapXep = new System.Windows.Forms.ComboBox();
            this.cbDinhDang = new System.Windows.Forms.ComboBox();
            this.cbQuocGia = new System.Windows.Forms.ComboBox();
            this.cbTheLoai = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // pnlPhim
            // 
            this.pnlPhim.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlPhim.AutoSize = true;
            this.pnlPhim.BackColor = System.Drawing.Color.Transparent;
            this.pnlPhim.Location = new System.Drawing.Point(56, 160);
            this.pnlPhim.Name = "pnlPhim";
            this.pnlPhim.Size = new System.Drawing.Size(883, 168);
            this.pnlPhim.TabIndex = 11;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(38)))), ((int)(((byte)(89)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(84)))), ((int)(((byte)(131)))), ((int)(((byte)(179)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(773, 47);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(140, 49);
            this.btnSearch.TabIndex = 10;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cbSapXep
            // 
            this.cbSapXep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSapXep.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSapXep.FormattingEnabled = true;
            this.cbSapXep.Location = new System.Drawing.Point(572, 56);
            this.cbSapXep.Name = "cbSapXep";
            this.cbSapXep.Size = new System.Drawing.Size(152, 33);
            this.cbSapXep.TabIndex = 9;
            // 
            // cbDinhDang
            // 
            this.cbDinhDang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDinhDang.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbDinhDang.FormattingEnabled = true;
            this.cbDinhDang.Location = new System.Drawing.Point(400, 56);
            this.cbDinhDang.Name = "cbDinhDang";
            this.cbDinhDang.Size = new System.Drawing.Size(152, 33);
            this.cbDinhDang.TabIndex = 8;
            // 
            // cbQuocGia
            // 
            this.cbQuocGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbQuocGia.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbQuocGia.FormattingEnabled = true;
            this.cbQuocGia.Location = new System.Drawing.Point(226, 56);
            this.cbQuocGia.Name = "cbQuocGia";
            this.cbQuocGia.Size = new System.Drawing.Size(152, 33);
            this.cbQuocGia.TabIndex = 7;
            // 
            // cbTheLoai
            // 
            this.cbTheLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTheLoai.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbTheLoai.FormattingEnabled = true;
            this.cbTheLoai.Location = new System.Drawing.Point(56, 56);
            this.cbTheLoai.Name = "cbTheLoai";
            this.cbTheLoai.Size = new System.Drawing.Size(152, 33);
            this.cbTheLoai.TabIndex = 6;
            // 
            // PhimLoai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(985, 448);
            this.Controls.Add(this.pnlPhim);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.cbSapXep);
            this.Controls.Add(this.cbDinhDang);
            this.Controls.Add(this.cbQuocGia);
            this.Controls.Add(this.cbTheLoai);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PhimLoai";
            this.Text = "PhimLoai";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.PhimLoai_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlPhim;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ComboBox cbSapXep;
        private System.Windows.Forms.ComboBox cbDinhDang;
        private System.Windows.Forms.ComboBox cbQuocGia;
        private System.Windows.Forms.ComboBox cbTheLoai;
    }
}