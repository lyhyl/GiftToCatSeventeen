using System;
using System.Windows.Forms;
using Anniversary;

namespace Anniversary
{
    public partial class MemorialDayEditor : Form
    {
        public MemorialDayEditor()
        {
            InitializeComponent();

            PathLabel.Text = "";
        }

        public MemorialDay Create()
        {
            Text = "新建";
            DateTimePicker.Value = DateTime.Now;

            switch (ShowDialog())
            {
                case DialogResult.OK:
                    return CreateNewMDay();
                default:
                    return null;
            }
        }

        private MemorialDay CreateNewMDay()
        {
            uint y = uint.Parse(DateTextBox.Text.Substring(0, 4));
            uint m = uint.Parse(DateTextBox.Text.Substring(5, 2));
            uint d = uint.Parse(DateTextBox.Text.Substring(8, 2));
            return new MemorialDay(y, m, d, TitleTextBox.Text, DescTextBox.Text, PathLabel.Text);
        }

        public void Edit(MemorialDay md)
        {
            Text = "编辑：" + md.Title;
            DateTextBox.Text = md.Date.ToString();
            DateTimePicker.Value = new DateTime((int)md.Date.Year, (int)md.Date.Month, (int)md.Date.Day);
            TitleTextBox.Text = md.Title;
            DescTextBox.Text = md.Description;

            switch (ShowDialog())
            {
                case DialogResult.OK:
                    md.Date.Year = uint.Parse(DateTextBox.Text.Substring(0, 4));
                    md.Date.Month = uint.Parse(DateTextBox.Text.Substring(5, 2));
                    md.Date.Day = uint.Parse(DateTextBox.Text.Substring(8, 2));
                    md.Title = TitleTextBox.Text;
                    md.Description = DescTextBox.Text;
                    break;
                default:
                    break;
            }
        }

        private void DateTypeChanged(object sender, EventArgs e)
        {
            DateTimePicker.Enabled = YMDTypeButton.Checked;
            DateTextBox.Mask = YMDTypeButton.Checked ? "0000年 90月 90日" : "90月 0周 90日";
        }

        private void DateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateTextBox.Text = DateTimePicker.Value.Year.ToString("D4") +
                DateTimePicker.Value.Month.ToString("D2") +
                DateTimePicker.Value.Day.ToString("D2");
        }

        private void TitleTextBox_TextChanged(object sender, EventArgs e)
        {
            Text = "编辑：" + TitleTextBox.Text;
        }

        private void SelectImageButton_Click(object sender, EventArgs e)
        {
            if (OpenImageDialog.ShowDialog() == DialogResult.OK)
                PathLabel.Text = OpenImageDialog.FileName;
        }
    }
}
