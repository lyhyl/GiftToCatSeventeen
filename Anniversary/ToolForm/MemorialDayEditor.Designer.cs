namespace Anniversary
{
    partial class MemorialDayEditor
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
            this.OkayButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.DateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.DateTextBox = new System.Windows.Forms.MaskedTextBox();
            this.TitleTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.DescTextBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.MWDTypeButton = new System.Windows.Forms.RadioButton();
            this.YMDTypeButton = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.SelectImageButton = new System.Windows.Forms.Button();
            this.PathLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.OpenImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // OkayButton
            // 
            this.OkayButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkayButton.Location = new System.Drawing.Point(15, 475);
            this.OkayButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.OkayButton.Name = "OkayButton";
            this.OkayButton.Size = new System.Drawing.Size(87, 33);
            this.OkayButton.TabIndex = 6;
            this.OkayButton.Text = "确定";
            this.OkayButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(274, 475);
            this.CancelButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(87, 33);
            this.CancelButton.TabIndex = 7;
            this.CancelButton.Text = "取消";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "标题:";
            // 
            // DateTimePicker
            // 
            this.DateTimePicker.Location = new System.Drawing.Point(55, 98);
            this.DateTimePicker.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DateTimePicker.Name = "DateTimePicker";
            this.DateTimePicker.Size = new System.Drawing.Size(285, 23);
            this.DateTimePicker.TabIndex = 3;
            this.DateTimePicker.ValueChanged += new System.EventHandler(this.DateTimePicker_ValueChanged);
            // 
            // DateTextBox
            // 
            this.DateTextBox.Location = new System.Drawing.Point(55, 59);
            this.DateTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DateTextBox.Mask = "0000年90月90日";
            this.DateTextBox.Name = "DateTextBox";
            this.DateTextBox.Size = new System.Drawing.Size(285, 23);
            this.DateTextBox.TabIndex = 2;
            this.DateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TitleTextBox
            // 
            this.TitleTextBox.Location = new System.Drawing.Point(55, 24);
            this.TitleTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TitleTextBox.Name = "TitleTextBox";
            this.TitleTextBox.Size = new System.Drawing.Size(285, 23);
            this.TitleTextBox.TabIndex = 4;
            this.TitleTextBox.TextChanged += new System.EventHandler(this.TitleTextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "日期:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.DescTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.TitleTextBox);
            this.groupBox1.Location = new System.Drawing.Point(14, 167);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(348, 198);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "描述";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "注释:";
            // 
            // DescTextBox
            // 
            this.DescTextBox.Location = new System.Drawing.Point(55, 62);
            this.DescTextBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DescTextBox.Multiline = true;
            this.DescTextBox.Name = "DescTextBox";
            this.DescTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DescTextBox.Size = new System.Drawing.Size(285, 126);
            this.DescTextBox.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.MWDTypeButton);
            this.groupBox2.Controls.Add(this.YMDTypeButton);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.DateTimePicker);
            this.groupBox2.Controls.Add(this.DateTextBox);
            this.groupBox2.Location = new System.Drawing.Point(14, 17);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(348, 142);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "时间";
            // 
            // MWDTypeButton
            // 
            this.MWDTypeButton.AutoSize = true;
            this.MWDTypeButton.Location = new System.Drawing.Point(127, 28);
            this.MWDTypeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MWDTypeButton.Name = "MWDTypeButton";
            this.MWDTypeButton.Size = new System.Drawing.Size(88, 21);
            this.MWDTypeButton.TabIndex = 1;
            this.MWDTypeButton.TabStop = true;
            this.MWDTypeButton.Text = "月-周-日 式";
            this.MWDTypeButton.UseVisualStyleBackColor = true;
            // 
            // YMDTypeButton
            // 
            this.YMDTypeButton.AutoSize = true;
            this.YMDTypeButton.Checked = true;
            this.YMDTypeButton.Location = new System.Drawing.Point(9, 28);
            this.YMDTypeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.YMDTypeButton.Name = "YMDTypeButton";
            this.YMDTypeButton.Size = new System.Drawing.Size(88, 21);
            this.YMDTypeButton.TabIndex = 0;
            this.YMDTypeButton.TabStop = true;
            this.YMDTypeButton.Text = "年-月-日 式";
            this.YMDTypeButton.UseVisualStyleBackColor = true;
            this.YMDTypeButton.CheckedChanged += new System.EventHandler(this.DateTypeChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.SelectImageButton);
            this.groupBox3.Controls.Add(this.PathLabel);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(14, 374);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(348, 92);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图片";
            // 
            // SelectImageButton
            // 
            this.SelectImageButton.Location = new System.Drawing.Point(253, 51);
            this.SelectImageButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.SelectImageButton.Name = "SelectImageButton";
            this.SelectImageButton.Size = new System.Drawing.Size(87, 33);
            this.SelectImageButton.TabIndex = 2;
            this.SelectImageButton.Text = "从文件选择";
            this.SelectImageButton.UseVisualStyleBackColor = true;
            this.SelectImageButton.Click += new System.EventHandler(this.SelectImageButton_Click);
            // 
            // PathLabel
            // 
            this.PathLabel.AutoSize = true;
            this.PathLabel.Location = new System.Drawing.Point(55, 24);
            this.PathLabel.Name = "PathLabel";
            this.PathLabel.Size = new System.Drawing.Size(39, 17);
            this.PathLabel.TabIndex = 1;
            this.PathLabel.Text = "NULL";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "路径:";
            // 
            // OpenImageDialog
            // 
            this.OpenImageDialog.Filter = "png|*.png|jpg|*.jpg|bmp|*.bmp";
            // 
            // MemorialDayEditor
            // 
            this.AcceptButton = this.OkayButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 520);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkayButton);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MemorialDayEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button OkayButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker DateTimePicker;
        private System.Windows.Forms.MaskedTextBox DateTextBox;
        private System.Windows.Forms.TextBox TitleTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton MWDTypeButton;
        private System.Windows.Forms.RadioButton YMDTypeButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox DescTextBox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button SelectImageButton;
        private System.Windows.Forms.Label PathLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog OpenImageDialog;
    }
}