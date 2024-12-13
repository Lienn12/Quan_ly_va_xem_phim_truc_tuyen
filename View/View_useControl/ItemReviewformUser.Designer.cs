namespace Quan_ly_thu_vien_phim.View.View_useControl
{
    partial class ItemReviewformUser
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
            this.txtReply = new System.Windows.Forms.TextBox();
            this.txtCmt = new System.Windows.Forms.TextBox();
            this.lbAdmin = new System.Windows.Forms.Label();
            this.lbRating = new System.Windows.Forms.Label();
            this.lbUser = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtReply
            // 
            this.txtReply.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtReply.Enabled = false;
            this.txtReply.Location = new System.Drawing.Point(66, 180);
            this.txtReply.Multiline = true;
            this.txtReply.Name = "txtReply";
            this.txtReply.Size = new System.Drawing.Size(302, 72);
            this.txtReply.TabIndex = 12;
            // 
            // txtCmt
            // 
            this.txtCmt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtCmt.Enabled = false;
            this.txtCmt.Location = new System.Drawing.Point(36, 69);
            this.txtCmt.Multiline = true;
            this.txtCmt.Name = "txtCmt";
            this.txtCmt.Size = new System.Drawing.Size(332, 72);
            this.txtCmt.TabIndex = 10;
            // 
            // lbAdmin
            // 
            this.lbAdmin.AutoSize = true;
            this.lbAdmin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbAdmin.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.lbAdmin.Location = new System.Drawing.Point(62, 156);
            this.lbAdmin.Name = "lbAdmin";
            this.lbAdmin.Size = new System.Drawing.Size(61, 21);
            this.lbAdmin.TabIndex = 9;
            this.lbAdmin.Text = "Admin";
            // 
            // lbRating
            // 
            this.lbRating.AutoSize = true;
            this.lbRating.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRating.Location = new System.Drawing.Point(295, 26);
            this.lbRating.Name = "lbRating";
            this.lbRating.Size = new System.Drawing.Size(63, 25);
            this.lbRating.TabIndex = 8;
            this.lbRating.Text = "label2";
            // 
            // lbUser
            // 
            this.lbUser.AutoSize = true;
            this.lbUser.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbUser.Location = new System.Drawing.Point(31, 22);
            this.lbUser.Name = "lbUser";
            this.lbUser.Size = new System.Drawing.Size(65, 25);
            this.lbUser.TabIndex = 7;
            this.lbUser.Text = "label1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Quan_ly_thu_vien_phim.Properties.Resources.star;
            this.pictureBox1.Location = new System.Drawing.Point(263, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(26, 34);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // ItemReviewformUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtReply);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCmt);
            this.Controls.Add(this.lbAdmin);
            this.Controls.Add(this.lbRating);
            this.Controls.Add(this.lbUser);
            this.Name = "ItemReviewformUser";
            this.Size = new System.Drawing.Size(402, 286);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtReply;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtCmt;
        private System.Windows.Forms.Label lbAdmin;
        private System.Windows.Forms.Label lbRating;
        private System.Windows.Forms.Label lbUser;
    }
}
