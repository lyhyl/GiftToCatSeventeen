using System;
using System.ComponentModel;
using System.Windows.Forms;
using Anniversary;

namespace Anniversary
{
    public partial class MemorialDayViewer : Form
    {
        private MemorialDayEditor editForm = new MemorialDayEditor();
        private MemorialDayCollection memorialDays = null;

        public MemorialDayCollection MemorialDays
        {
            get { return memorialDays; }
            set
            {
                memorialDays = value;
                UpdateListView();
            }
        }

        public MemorialDayViewer()
        {
            InitializeComponent();
        }

        private void UpdateListView()
        {
            if (memorialDays == null)
                return;
            foreach (MemorialDay d in memorialDays)
                DayListView.Items.Add(d.GetListViewItem());
        }

        private void NewDate()
        {
            MemorialDay md = editForm.Create();
            if (md != null)
            {
                memorialDays.Add(md);
                DayListView.Items.Add(md.GetListViewItem());
            }
        }

        private void EditDate()
        {
            if (DayListView.SelectedItems.Count > 0)
            {
                MemorialDay md = DayListView.SelectedItems[0].Tag as MemorialDay;
                if (md != null)
                {
                    editForm.Edit(md);
                    md.Update();
                }
            }
        }

        private void DeleteDate()
        {
            if (MessageBox.Show("确定删除此纪念日？", "确认",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                foreach (ListViewItem item in DayListView.SelectedItems)
                {
                    DayListView.Items.Remove(item);
                    memorialDays.Remove(item.Tag as MemorialDay);
                }
        }

        private ListViewGroup GetOrCreateGroup(string name)
        {
            ListViewGroup lvg = SreachListViewGroup(name);
            if (lvg == null)
            {
                lvg = new ListViewGroup(name);
                DayListView.Groups.Add(lvg);
            }
            return lvg;
        }

        private void OrderListViewGroup()
        {
            ListViewGroup[] alvg = new ListViewGroup[DayListView.Groups.Count];
            DayListView.Groups.CopyTo(alvg, 0);
            Array.Sort(alvg, (ListViewGroup lvga, ListViewGroup lvgb) => { return lvga.Header.CompareTo(lvgb.Header); });
            DayListView.Groups.Clear();
            DayListView.Groups.AddRange(alvg);
        }

        private ListViewGroup SreachListViewGroup(string name)
        {
            foreach (ListViewGroup gp in DayListView.Groups)
                if (gp.Header == name)
                    return gp;
            return null;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            Hide();
        }

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDate();
        }

        private void EditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditDate();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteDate();
        }

        private void DayListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            DayListView.Groups.Clear();
            foreach (ListViewItem item in DayListView.Items)
                item.Group = GetOrCreateGroup(item.SubItems[e.Column].Text);
            OrderListViewGroup();
        }
    }
}
