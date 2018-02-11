namespace GiftToCatSeventeen
{
    partial class PluginForm
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
            this.HideDelayTimer = new System.Windows.Forms.Timer(this.components);
            this.PrevPagePluginBtn = new System.Windows.Forms.Button();
            this.NextPagePluginBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // HideDelayTimer
            //
            this.HideDelayTimer.Interval = 1000;
            this.HideDelayTimer.Tick += new System.EventHandler(this.DelayTimer_Tick);
            // 
            // PrevPagePluginBtn
            // 
            this.PrevPagePluginBtn.Location = new System.Drawing.Point(0, 0);
            this.PrevPagePluginBtn.Name = "PrevPagePluginBtn";
            this.PrevPagePluginBtn.Size = new System.Drawing.Size(24, 24);
            this.PrevPagePluginBtn.TabIndex = 0;
            // 
            // NextPagePluginBtn
            // 
            this.NextPagePluginBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.NextPagePluginBtn.FlatAppearance.BorderSize = 0;
            this.NextPagePluginBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.NextPagePluginBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.NextPagePluginBtn.Location = new System.Drawing.Point(276, 0);
            this.NextPagePluginBtn.Name = "NextPagePluginBtn";
            this.NextPagePluginBtn.Size = new System.Drawing.Size(24, 24);
            this.NextPagePluginBtn.TabIndex = 1;
            // 
            // PluginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(300, 26);
            this.ControlBox = false;
            this.Controls.Add(this.NextPagePluginBtn);
            this.Controls.Add(this.PrevPagePluginBtn);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1, 1);
            this.Name = "PluginForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "PluginForm";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.DarkSlateGray;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer HideDelayTimer;
        private System.Windows.Forms.Button PrevPagePluginBtn;
        private System.Windows.Forms.Button NextPagePluginBtn;

    }
}