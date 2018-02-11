namespace GTCSInstaller
{
    partial class Installer
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
            this.MyWord = new System.Windows.Forms.Label();
            this.InstallProgressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // MyWord
            // 
            this.MyWord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MyWord.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.MyWord.Location = new System.Drawing.Point(0, 0);
            this.MyWord.Name = "MyWord";
            this.MyWord.Size = new System.Drawing.Size(296, 30);
            this.MyWord.TabIndex = 0;
            this.MyWord.Text = ".";
            this.MyWord.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // InstallProgressBar
            // 
            this.InstallProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.InstallProgressBar.Location = new System.Drawing.Point(0, 30);
            this.InstallProgressBar.Name = "InstallProgressBar";
            this.InstallProgressBar.Size = new System.Drawing.Size(296, 16);
            this.InstallProgressBar.TabIndex = 1;
            // 
            // Installer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 46);
            this.ControlBox = false;
            this.Controls.Add(this.MyWord);
            this.Controls.Add(this.InstallProgressBar);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Installer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Gift To Cat Seventeen";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label MyWord;
        private System.Windows.Forms.ProgressBar InstallProgressBar;
    }
}

