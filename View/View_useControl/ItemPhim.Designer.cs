namespace Quan_ly_thu_vien_phim.View.View_Main
{
    partial class ItemPhim
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
            this.lbVip = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbVip
            // 
            this.lbVip.AutoSize = true;
            this.lbVip.BackColor = System.Drawing.SystemColors.Info;
            this.lbVip.Font = new System.Drawing.Font("Segoe UI", 11.12727F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbVip.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lbVip.Location = new System.Drawing.Point(16, 19);
            this.lbVip.Name = "lbVip";
            this.lbVip.Size = new System.Drawing.Size(35, 23);
            this.lbVip.TabIndex = 0;
            this.lbVip.Text = "Vip";
            // 
            // ItemPhim
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbVip);
            this.Name = "ItemPhim";
            this.Size = new System.Drawing.Size(243, 304);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbVip;
    }
}
