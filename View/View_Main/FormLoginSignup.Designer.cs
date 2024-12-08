namespace Quan_ly_thu_vien_phim.View.View_Main
{
    partial class FormLoginSignup
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
            this.pnlLogSign = new System.Windows.Forms.Panel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbMinimum = new System.Windows.Forms.Label();
            this.lbExit = new System.Windows.Forms.Label();
            this.pnlLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLogSign
            // 
            this.pnlLogSign.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLogSign.Location = new System.Drawing.Point(344, 32);
            this.pnlLogSign.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogSign.Name = "pnlLogSign";
            this.pnlLogSign.Size = new System.Drawing.Size(456, 568);
            this.pnlLogSign.TabIndex = 0;
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(38)))), ((int)(((byte)(89)))));
            this.pnlLogo.Controls.Add(this.label1);
            this.pnlLogo.Controls.Add(this.pictureBox1);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(344, 600);
            this.pnlLogo.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(19, 342);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 37);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quản lý thư viện phim";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Quan_ly_thu_vien_phim.Properties.Resources.Logo__3_;
            this.pictureBox1.Location = new System.Drawing.Point(60, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(213, 207);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lbMinimum);
            this.panel1.Controls.Add(this.lbExit);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(344, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(456, 32);
            this.panel1.TabIndex = 0;
            // 
            // lbMinimum
            // 
            this.lbMinimum.AutoSize = true;
            this.lbMinimum.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbMinimum.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbMinimum.Font = new System.Drawing.Font("Segoe UI Black", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbMinimum.Location = new System.Drawing.Point(402, 0);
            this.lbMinimum.Name = "lbMinimum";
            this.lbMinimum.Size = new System.Drawing.Size(24, 32);
            this.lbMinimum.TabIndex = 1;
            this.lbMinimum.Text = "_";
            this.lbMinimum.Click += new System.EventHandler(this.lbMinimum_Click);
            // 
            // lbExit
            // 
            this.lbExit.AutoSize = true;
            this.lbExit.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lbExit.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbExit.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbExit.Location = new System.Drawing.Point(426, 0);
            this.lbExit.Name = "lbExit";
            this.lbExit.Size = new System.Drawing.Size(30, 32);
            this.lbExit.TabIndex = 0;
            this.lbExit.Text = "X";
            this.lbExit.Click += new System.EventHandler(this.lbExit_Click);
            // 
            // FormLoginSignup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.pnlLogSign);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlLogo);
            this.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(38)))), ((int)(((byte)(89)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormLoginSignup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormLoginSignup";
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLogSign;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbExit;
        private System.Windows.Forms.Label lbMinimum;
    }
}