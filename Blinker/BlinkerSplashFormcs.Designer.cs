namespace Blinker
{
    partial class BlinkerSplashForm
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
            this.components = new System.ComponentModel.Container();
            this.SplashImage = new System.Windows.Forms.PictureBox();
            this.SplashTimer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.SplashImage)).BeginInit();
            this.SuspendLayout();
            // 
            // SplashImage
            // 
            this.SplashImage.Location = new System.Drawing.Point(0, 0);
            this.SplashImage.Name = "SplashImage";
            this.SplashImage.Size = new System.Drawing.Size(360, 160);
            this.SplashImage.TabIndex = 0;
            this.SplashImage.TabStop = false;
            this.SplashImage.Click += new System.EventHandler(this.SplashImage_Click);
            // 
            // SplashTimer
            // 
            this.SplashTimer.Tick += new System.EventHandler(this.SplashTimer_Tick);
            // 
            // BlinkerSplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(360, 160);
            this.Controls.Add(this.SplashImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "BlinkerSplashForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.BlinkerSplashForm_Load);
            this.Shown += new System.EventHandler(this.BlinkerSplashForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.SplashImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox SplashImage;
        private System.Windows.Forms.Timer SplashTimer;
    }
}