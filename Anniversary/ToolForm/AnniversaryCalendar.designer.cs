namespace Anniversary
{
    partial class AnniversaryCalendar
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
            this.SetDayBtn = new System.Windows.Forms.Button();
            this.PrevYearBtn = new System.Windows.Forms.Button();
            this.NextYearBtn = new System.Windows.Forms.Button();
            this.NextMonthBtn = new System.Windows.Forms.Button();
            this.PrevMonthBtn = new System.Windows.Forms.Button();
            this.CloseBtn = new System.Windows.Forms.Button();
            this.MDayDescTip = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // SetDayBtn
            // 
            this.SetDayBtn.BackColor = System.Drawing.SystemColors.ControlDark;
            this.SetDayBtn.FlatAppearance.BorderSize = 0;
            this.SetDayBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SetDayBtn.Font = new System.Drawing.Font("SimSun", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SetDayBtn.Location = new System.Drawing.Point(0, 0);
            this.SetDayBtn.Name = "SetDayBtn";
            this.SetDayBtn.Size = new System.Drawing.Size(16, 16);
            this.SetDayBtn.TabIndex = 0;
            this.SetDayBtn.Text = "S";
            this.SetDayBtn.UseVisualStyleBackColor = false;
            this.SetDayBtn.Click += new System.EventHandler(this.SetDayBtn_Click);
            // 
            // PrevYearBtn
            // 
            this.PrevYearBtn.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PrevYearBtn.FlatAppearance.BorderSize = 0;
            this.PrevYearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrevYearBtn.Font = new System.Drawing.Font("SimSun", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PrevYearBtn.Location = new System.Drawing.Point(0, 26);
            this.PrevYearBtn.Name = "PrevYearBtn";
            this.PrevYearBtn.Size = new System.Drawing.Size(16, 16);
            this.PrevYearBtn.TabIndex = 1;
            this.PrevYearBtn.Text = "P";
            this.PrevYearBtn.UseVisualStyleBackColor = false;
            this.PrevYearBtn.Click += new System.EventHandler(this.PrevYearBtn_Click);
            // 
            // NextYearBtn
            // 
            this.NextYearBtn.BackColor = System.Drawing.SystemColors.ControlDark;
            this.NextYearBtn.FlatAppearance.BorderSize = 0;
            this.NextYearBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextYearBtn.Font = new System.Drawing.Font("SimSun", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NextYearBtn.Location = new System.Drawing.Point(26, 26);
            this.NextYearBtn.Name = "NextYearBtn";
            this.NextYearBtn.Size = new System.Drawing.Size(16, 16);
            this.NextYearBtn.TabIndex = 2;
            this.NextYearBtn.Text = "N";
            this.NextYearBtn.UseVisualStyleBackColor = false;
            this.NextYearBtn.Click += new System.EventHandler(this.NextYearBtn_Click);
            // 
            // NextMonthBtn
            // 
            this.NextMonthBtn.BackColor = System.Drawing.SystemColors.ControlDark;
            this.NextMonthBtn.FlatAppearance.BorderSize = 0;
            this.NextMonthBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextMonthBtn.Font = new System.Drawing.Font("SimSun", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.NextMonthBtn.Location = new System.Drawing.Point(26, 52);
            this.NextMonthBtn.Name = "NextMonthBtn";
            this.NextMonthBtn.Size = new System.Drawing.Size(16, 16);
            this.NextMonthBtn.TabIndex = 4;
            this.NextMonthBtn.Text = "N";
            this.NextMonthBtn.UseVisualStyleBackColor = false;
            this.NextMonthBtn.Click += new System.EventHandler(this.NextMonthBtn_Click);
            // 
            // PrevMonthBtn
            // 
            this.PrevMonthBtn.BackColor = System.Drawing.SystemColors.ControlDark;
            this.PrevMonthBtn.FlatAppearance.BorderSize = 0;
            this.PrevMonthBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PrevMonthBtn.Font = new System.Drawing.Font("SimSun", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.PrevMonthBtn.Location = new System.Drawing.Point(0, 52);
            this.PrevMonthBtn.Name = "PrevMonthBtn";
            this.PrevMonthBtn.Size = new System.Drawing.Size(16, 16);
            this.PrevMonthBtn.TabIndex = 3;
            this.PrevMonthBtn.Text = "P";
            this.PrevMonthBtn.UseVisualStyleBackColor = false;
            this.PrevMonthBtn.Click += new System.EventHandler(this.PrevMonthBtn_Click);
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackColor = System.Drawing.SystemColors.ControlDark;
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.Font = new System.Drawing.Font("SimSun", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.CloseBtn.Location = new System.Drawing.Point(0, 74);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(16, 16);
            this.CloseBtn.TabIndex = 5;
            this.CloseBtn.Text = "X";
            this.CloseBtn.UseVisualStyleBackColor = false;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // AnniversaryCalendar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.ControlBox = false;
            this.Controls.Add(this.CloseBtn);
            this.Controls.Add(this.NextMonthBtn);
            this.Controls.Add(this.PrevMonthBtn);
            this.Controls.Add(this.NextYearBtn);
            this.Controls.Add(this.PrevYearBtn);
            this.Controls.Add(this.SetDayBtn);
            this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AnniversaryCalendar";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "纪念日";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SetDayBtn;
        private System.Windows.Forms.Button PrevYearBtn;
        private System.Windows.Forms.Button NextYearBtn;
        private System.Windows.Forms.Button NextMonthBtn;
        private System.Windows.Forms.Button PrevMonthBtn;
        private System.Windows.Forms.Button CloseBtn;
        private System.Windows.Forms.ToolTip MDayDescTip;
    }
}