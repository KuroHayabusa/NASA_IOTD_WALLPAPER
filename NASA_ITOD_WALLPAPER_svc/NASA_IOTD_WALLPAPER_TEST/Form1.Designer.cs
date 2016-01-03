namespace NASA_IOTD_WALLPAPER_TEST
{
    partial class Form1
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
            this.CacheImageButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CacheImageButton
            // 
            this.CacheImageButton.Location = new System.Drawing.Point(38, 42);
            this.CacheImageButton.Name = "CacheImageButton";
            this.CacheImageButton.Size = new System.Drawing.Size(156, 58);
            this.CacheImageButton.TabIndex = 0;
            this.CacheImageButton.Text = "Cache Images";
            this.CacheImageButton.UseVisualStyleBackColor = true;
            this.CacheImageButton.Click += new System.EventHandler(this.CacheImageButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 253);
            this.Controls.Add(this.CacheImageButton);
            this.Name = "Form1";
            this.Text = "NASA Image of the Day Test App";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CacheImageButton;
    }
}

