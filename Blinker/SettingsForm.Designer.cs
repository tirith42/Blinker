namespace Blinker
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.DriveLabel = new System.Windows.Forms.Label();
            this.DriveCombo = new System.Windows.Forms.ComboBox();
            this.DoneButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ColorLabel = new System.Windows.Forms.Label();
            this.ColorCombo = new System.Windows.Forms.ComboBox();
            this.PreviewImage = new System.Windows.Forms.PictureBox();
            this.SplashCheck = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.PreviewImage)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(274, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a drive to monitor, LED color, and anything else that might be here.";
            // 
            // DriveLabel
            // 
            this.DriveLabel.AutoSize = true;
            this.DriveLabel.Location = new System.Drawing.Point(13, 96);
            this.DriveLabel.Name = "DriveLabel";
            this.DriveLabel.Size = new System.Drawing.Size(85, 13);
            this.DriveLabel.TabIndex = 4;
            this.DriveLabel.Text = "&Drive to Monitor:";
            // 
            // DriveCombo
            // 
            this.DriveCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DriveCombo.FormattingEnabled = true;
            this.DriveCombo.Location = new System.Drawing.Point(104, 93);
            this.DriveCombo.Name = "DriveCombo";
            this.DriveCombo.Size = new System.Drawing.Size(63, 21);
            this.DriveCombo.Sorted = true;
            this.DriveCombo.TabIndex = 5;
            this.DriveCombo.SelectedIndexChanged += new System.EventHandler(this.DriveCombo_SelectedIndexChanged);
            // 
            // DoneButton
            // 
            this.DoneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DoneButton.Location = new System.Drawing.Point(131, 174);
            this.DoneButton.Name = "DoneButton";
            this.DoneButton.Size = new System.Drawing.Size(75, 23);
            this.DoneButton.TabIndex = 6;
            this.DoneButton.Text = "&save";
            this.DoneButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.DoneButton.UseVisualStyleBackColor = true;
            this.DoneButton.Click += new System.EventHandler(this.DoneButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.Location = new System.Drawing.Point(212, 174);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "&cancel";
            this.CancelButton.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ColorLabel
            // 
            this.ColorLabel.AutoSize = true;
            this.ColorLabel.Location = new System.Drawing.Point(13, 69);
            this.ColorLabel.Name = "ColorLabel";
            this.ColorLabel.Size = new System.Drawing.Size(58, 13);
            this.ColorLabel.TabIndex = 8;
            this.ColorLabel.Text = "&LED Color:";
            // 
            // ColorCombo
            // 
            this.ColorCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ColorCombo.FormattingEnabled = true;
            this.ColorCombo.Location = new System.Drawing.Point(104, 66);
            this.ColorCombo.Name = "ColorCombo";
            this.ColorCombo.Size = new System.Drawing.Size(63, 21);
            this.ColorCombo.TabIndex = 9;
            this.ColorCombo.SelectedIndexChanged += new System.EventHandler(this.ColorCombo_SelectedIndexChanged);
            // 
            // PreviewImage
            // 
            this.PreviewImage.Location = new System.Drawing.Point(174, 69);
            this.PreviewImage.Name = "PreviewImage";
            this.PreviewImage.Size = new System.Drawing.Size(16, 16);
            this.PreviewImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PreviewImage.TabIndex = 10;
            this.PreviewImage.TabStop = false;
            // 
            // SplashCheck
            // 
            this.SplashCheck.AutoSize = true;
            this.SplashCheck.Location = new System.Drawing.Point(16, 135);
            this.SplashCheck.Name = "SplashCheck";
            this.SplashCheck.Size = new System.Drawing.Size(175, 17);
            this.SplashCheck.TabIndex = 11;
            this.SplashCheck.Text = "Show splash &window on startup";
            this.SplashCheck.UseVisualStyleBackColor = true;
            this.SplashCheck.CheckedChanged += new System.EventHandler(this.SplashCheck_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Location = new System.Drawing.Point(-1, 163);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(301, 46);
            this.panel1.TabIndex = 12;
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(299, 208);
            this.Controls.Add(this.SplashCheck);
            this.Controls.Add(this.PreviewImage);
            this.Controls.Add(this.ColorCombo);
            this.Controls.Add(this.ColorLabel);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DoneButton);
            this.Controls.Add(this.DriveCombo);
            this.Controls.Add(this.DriveLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blinker Settings";
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PreviewImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label DriveLabel;
        private System.Windows.Forms.ComboBox DriveCombo;
        private System.Windows.Forms.Button DoneButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label ColorLabel;
        private System.Windows.Forms.ComboBox ColorCombo;
        private System.Windows.Forms.PictureBox PreviewImage;
        private System.Windows.Forms.CheckBox SplashCheck;
        private System.Windows.Forms.Panel panel1;
    }
}