namespace Anniversary
{
    partial class MemorialDayViewer
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
            this.OperateMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DayListView = new System.Windows.Forms.ListView();
            this.yearColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.monthColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dayColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.titleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.descColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.OperateMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // OperateMenu
            // 
            this.OperateMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.DeleteToolStripMenuItem});
            this.OperateMenu.Name = "OperateMenu";
            this.OperateMenu.Size = new System.Drawing.Size(95, 70);
            // 
            // NewToolStripMenuItem
            // 
            this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
            this.NewToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.NewToolStripMenuItem.Text = "新建";
            this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
            // 
            // EditToolStripMenuItem
            // 
            this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
            this.EditToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.EditToolStripMenuItem.Text = "编辑";
            this.EditToolStripMenuItem.Click += new System.EventHandler(this.EditToolStripMenuItem_Click);
            // 
            // DeleteToolStripMenuItem
            // 
            this.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem";
            this.DeleteToolStripMenuItem.Size = new System.Drawing.Size(94, 22);
            this.DeleteToolStripMenuItem.Text = "删除";
            this.DeleteToolStripMenuItem.Click += new System.EventHandler(this.DeleteToolStripMenuItem_Click);
            // 
            // DayListView
            // 
            this.DayListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.DayListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.yearColumnHeader,
            this.monthColumnHeader,
            this.dayColumnHeader,
            this.titleColumnHeader,
            this.descColumnHeader,
            this.imageColumnHeader});
            this.DayListView.ContextMenuStrip = this.OperateMenu;
            this.DayListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DayListView.Location = new System.Drawing.Point(0, 0);
            this.DayListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.DayListView.Name = "DayListView";
            this.DayListView.Size = new System.Drawing.Size(574, 235);
            this.DayListView.TabIndex = 1;
            this.DayListView.UseCompatibleStateImageBehavior = false;
            this.DayListView.View = System.Windows.Forms.View.Details;
            this.DayListView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.DayListView_ColumnClick);
            // 
            // yearColumnHeader
            // 
            this.yearColumnHeader.Text = "年";
            // 
            // monthColumnHeader
            // 
            this.monthColumnHeader.Text = "月";
            // 
            // dayColumnHeader
            // 
            this.dayColumnHeader.Text = "日";
            // 
            // titleColumnHeader
            // 
            this.titleColumnHeader.Text = "标题";
            // 
            // descColumnHeader
            // 
            this.descColumnHeader.Text = "描述";
            this.descColumnHeader.Width = 175;
            // 
            // imageColumnHeader
            // 
            this.imageColumnHeader.Text = "图片";
            // 
            // MemorialDayViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 235);
            this.Controls.Add(this.DayListView);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MemorialDayViewer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "设置纪念日";
            this.OperateMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip OperateMenu;
        private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuItem;
        private System.Windows.Forms.ListView DayListView;
        private System.Windows.Forms.ColumnHeader yearColumnHeader;
        private System.Windows.Forms.ColumnHeader monthColumnHeader;
        private System.Windows.Forms.ColumnHeader dayColumnHeader;
        private System.Windows.Forms.ColumnHeader titleColumnHeader;
        private System.Windows.Forms.ColumnHeader descColumnHeader;
        private System.Windows.Forms.ColumnHeader imageColumnHeader;
    }
}