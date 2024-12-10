namespace Quan_ly_thu_vien_phim.View.View_Main
{
    partial class FormMainUser
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lbMinimum = new System.Windows.Forms.Label();
            this.lbExit = new System.Windows.Forms.Label();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnCaNhan = new System.Windows.Forms.Button();
            this.btnFavourite = new System.Windows.Forms.Button();
            this.btnHome = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlHeader.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(38)))), ((int)(((byte)(89)))));
            this.pnlHeader.Controls.Add(this.lbMinimum);
            this.pnlHeader.Controls.Add(this.lbExit);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1875, 62);
            this.pnlHeader.TabIndex = 0;
            // 
            // lbMinimum
            // 
            this.lbMinimum.AutoSize = true;
            this.lbMinimum.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbMinimum.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbMinimum.Font = new System.Drawing.Font("Segoe UI Black", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMinimum.ForeColor = System.Drawing.Color.White;
            this.lbMinimum.Location = new System.Drawing.Point(1811, 0);
            this.lbMinimum.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbMinimum.Name = "lbMinimum";
            this.lbMinimum.Size = new System.Drawing.Size(29, 40);
            this.lbMinimum.TabIndex = 2;
            this.lbMinimum.Text = "_";
            this.lbMinimum.Click += new System.EventHandler(this.lbMinimum_Click);
            // 
            // lbExit
            // 
            this.lbExit.AutoSize = true;
            this.lbExit.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbExit.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExit.ForeColor = System.Drawing.Color.White;
            this.lbExit.Location = new System.Drawing.Point(1840, 0);
            this.lbExit.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbExit.Name = "lbExit";
            this.lbExit.Size = new System.Drawing.Size(35, 37);
            this.lbExit.TabIndex = 3;
            this.lbExit.Text = "X";
            this.lbExit.Click += new System.EventHandler(this.lbExit_Click);
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(38)))), ((int)(((byte)(89)))));
            this.pnlMenu.Controls.Add(this.btnLogin);
            this.pnlMenu.Controls.Add(this.btnCaNhan);
            this.pnlMenu.Controls.Add(this.btnFavourite);
            this.pnlMenu.Controls.Add(this.btnHome);
            this.pnlMenu.Controls.Add(this.panel2);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.pnlMenu.Location = new System.Drawing.Point(0, 62);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(375, 1038);
            this.pnlMenu.TabIndex = 1;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.Transparent;
            this.btnLogin.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(160)))), ((int)(((byte)(202)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Image = global::Quan_ly_thu_vien_phim.Properties.Resources.logout;
            this.btnLogin.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.Location = new System.Drawing.Point(0, 710);
            this.btnLogin.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnLogin.Size = new System.Drawing.Size(375, 92);
            this.btnLogin.TabIndex = 11;
            this.btnLogin.Text = "      Log out";
            this.btnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnCaNhan
            // 
            this.btnCaNhan.BackColor = System.Drawing.Color.Transparent;
            this.btnCaNhan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCaNhan.FlatAppearance.BorderSize = 0;
            this.btnCaNhan.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(160)))), ((int)(((byte)(202)))));
            this.btnCaNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaNhan.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaNhan.ForeColor = System.Drawing.Color.White;
            this.btnCaNhan.Image = global::Quan_ly_thu_vien_phim.Properties.Resources.user;
            this.btnCaNhan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCaNhan.Location = new System.Drawing.Point(0, 618);
            this.btnCaNhan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCaNhan.Name = "btnCaNhan";
            this.btnCaNhan.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnCaNhan.Size = new System.Drawing.Size(375, 92);
            this.btnCaNhan.TabIndex = 10;
            this.btnCaNhan.Text = "      TT Cá nhân";
            this.btnCaNhan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCaNhan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCaNhan.UseVisualStyleBackColor = false;
            this.btnCaNhan.Click += new System.EventHandler(this.btnCaNhan_Click);
            // 
            // btnFavourite
            // 
            this.btnFavourite.BackColor = System.Drawing.Color.Transparent;
            this.btnFavourite.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFavourite.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnFavourite.FlatAppearance.BorderSize = 0;
            this.btnFavourite.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(160)))), ((int)(((byte)(202)))));
            this.btnFavourite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFavourite.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFavourite.ForeColor = System.Drawing.Color.White;
            this.btnFavourite.Image = global::Quan_ly_thu_vien_phim.Properties.Resources.heart_icon;
            this.btnFavourite.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFavourite.Location = new System.Drawing.Point(0, 526);
            this.btnFavourite.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFavourite.Name = "btnFavourite";
            this.btnFavourite.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnFavourite.Size = new System.Drawing.Size(375, 92);
            this.btnFavourite.TabIndex = 9;
            this.btnFavourite.Text = "      Yêu thích";
            this.btnFavourite.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFavourite.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnFavourite.UseVisualStyleBackColor = false;
            this.btnFavourite.Click += new System.EventHandler(this.btnFavourite_Click);
            // 
            // btnHome
            // 
            this.btnHome.BackColor = System.Drawing.Color.Transparent;
            this.btnHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHome.FlatAppearance.BorderSize = 0;
            this.btnHome.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(160)))), ((int)(((byte)(202)))));
            this.btnHome.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHome.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHome.ForeColor = System.Drawing.Color.White;
            this.btnHome.Image = global::Quan_ly_thu_vien_phim.Properties.Resources.home;
            this.btnHome.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.Location = new System.Drawing.Point(0, 434);
            this.btnHome.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHome.Name = "btnHome";
            this.btnHome.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnHome.Size = new System.Drawing.Size(375, 92);
            this.btnHome.TabIndex = 8;
            this.btnHome.Text = "      Trang chủ";
            this.btnHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHome.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnHome.UseVisualStyleBackColor = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(375, 434);
            this.panel2.TabIndex = 12;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox2.Image = global::Quan_ly_thu_vien_phim.Properties.Resources.avatar;
            this.pictureBox2.Location = new System.Drawing.Point(53, 77);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(264, 251);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(375, 62);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1500, 1038);
            this.pnlMain.TabIndex = 2;
            // 
            // FormMainUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1875, 1100);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMainUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMainUser";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label lbMinimum;
        private System.Windows.Forms.Label lbExit;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnCaNhan;
        private System.Windows.Forms.Button btnFavourite;
        private System.Windows.Forms.Button btnHome;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}