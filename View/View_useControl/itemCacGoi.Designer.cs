namespace Quan_ly_thu_vien_phim.View.View_useControl
{
    partial class itemCacGoi
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
            this.lbPrice = new System.Windows.Forms.Label();
            this.CacGoi = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // lbPrice
            // 
            this.lbPrice.Dock = System.Windows.Forms.DockStyle.Right;
            this.lbPrice.Font = new System.Drawing.Font("Segoe UI", 13.74545F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(38)))), ((int)(((byte)(89)))));
            this.lbPrice.Location = new System.Drawing.Point(186, 0);
            this.lbPrice.Margin = new System.Windows.Forms.Padding(5);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(132, 73);
            this.lbPrice.TabIndex = 1;
            this.lbPrice.Text = "label2";
            this.lbPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CacGoi
            // 
            this.CacGoi.AutoSize = true;
            this.CacGoi.Dock = System.Windows.Forms.DockStyle.Left;
            this.CacGoi.Font = new System.Drawing.Font("Segoe UI", 13.74545F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CacGoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(38)))), ((int)(((byte)(89)))));
            this.CacGoi.Location = new System.Drawing.Point(0, 0);
            this.CacGoi.Name = "CacGoi";
            this.CacGoi.Size = new System.Drawing.Size(151, 73);
            this.CacGoi.TabIndex = 2;
            this.CacGoi.TabStop = true;
            this.CacGoi.Text = "radioButton1";
            this.CacGoi.UseVisualStyleBackColor = true;
            this.CacGoi.CheckedChanged += new System.EventHandler(this.CacGoi_CheckedChanged);
            // 
            // itemCacGoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.CacGoi);
            this.Controls.Add(this.lbPrice);
            this.Name = "itemCacGoi";
            this.Size = new System.Drawing.Size(318, 73);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.RadioButton CacGoi;
    }
}
