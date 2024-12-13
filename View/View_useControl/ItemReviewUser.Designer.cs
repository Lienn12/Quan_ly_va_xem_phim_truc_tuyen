namespace Quan_ly_thu_vien_phim.View.View_useControl
{
    partial class ItemReviewUser
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbUser = new System.Windows.Forms.Label();
            this.lbRating = new System.Windows.Forms.Label();
            this.lbAdmin = new System.Windows.Forms.Label();
            this.txtCmt = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtReply = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbUser
            // 
            this.lbUser.AutoSize = true;
            this.lbUser.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUser.Location = new System.Drawing.Point(24, 14);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(65, 25);
            this.lbUser.TabIndex = 0;
            this.lbUser.Text = "label1";
            // 
            // lbRating
            // 
            this.lbRating.AutoSize = true;
            this.lbRating.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRating.Location = new System.Drawing.Point(288, 18);
            this.lbRating.Name = "lbRating";
            this.lbRating.Size = new System.Drawing.Size(63, 25);
            this.lbRating.TabIndex = 1;
            this.lbRating.Text = "label2";
            // 
            // lbAdmin
            // 
            this.lbAdmin.AutoSize = true;
            this.lbAdmin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAdmin.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbAdmin.Location = new System.Drawing.Point(55, 148);
            this.lbAdmin.Name = "lbAdmin";
            this.lbAdmin.Size = new System.Drawing.Size(61, 21);
            this.lbAdmin.TabIndex = 2;
            this.lbAdmin.Text = "Admin";
            // 
            // txtCmt
            // 
            this.txtCmt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCmt.Enabled = false;
            this.txtCmt.Location = new System.Drawing.Point(29, 61);
            this.txtCmt.Multiline = true;
            this.txtCmt.Name = "txtCmt";
            this.txtCmt.Size = new System.Drawing.Size(332, 72);
            this.txtCmt.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Quan_ly_thu_vien_phim.Properties.Resources.star;
            this.pictureBox1.Location = new System.Drawing.Point(256, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 34);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // txtReply
            // 
            this.txtReply.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReply.Enabled = false;
            this.txtReply.Location = new System.Drawing.Point(59, 172);
            this.txtReply.Multiline = true;
            this.txtReply.Name = "txtReply";
            this.txtReply.Size = new System.Drawing.Size(302, 72);
            this.txtReply.TabIndex = 6;
            // 
            // ItiemReviewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtReply);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCmt);
            this.Controls.Add(this.lbAdmin);
            this.Controls.Add(this.lbRating);
            this.Controls.Add(this.lbUser);
            this.Name = "ItiemReviewUser";
            this.Size = new System.Drawing.Size(405, 261);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbUser;
        private System.Windows.Forms.Label lbRating;
        private System.Windows.Forms.Label lbAdmin;
        private System.Windows.Forms.TextBox txtCmt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtReply;
    }
}
