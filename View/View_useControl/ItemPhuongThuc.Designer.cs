namespace Quan_ly_thu_vien_phim.View.View_useControl
{
    partial class ItemPhuongThuc
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
            this.rPT = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // rPT
            // 
            this.rPT.AutoSize = true;
            this.rPT.Dock = System.Windows.Forms.DockStyle.Left;
            this.rPT.Font = new System.Drawing.Font("Segoe UI", 13.74545F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rPT.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(38)))), ((int)(((byte)(89)))));
            this.rPT.Location = new System.Drawing.Point(0, 0);
            this.rPT.Name = "rPT";
            this.rPT.Size = new System.Drawing.Size(151, 73);
            this.rPT.TabIndex = 0;
            this.rPT.TabStop = true;
            this.rPT.Text = "radioButton1";
            this.rPT.UseVisualStyleBackColor = true;
            this.rPT.CheckedChanged += new System.EventHandler(this.rPT_CheckedChanged);
            // 
            // ItemPhuongThuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rPT);
            this.Name = "ItemPhuongThuc";
            this.Size = new System.Drawing.Size(318, 73);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rPT;
    }
}
