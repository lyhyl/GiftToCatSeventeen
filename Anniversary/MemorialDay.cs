using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Anniversary
{
    [Serializable]
    public class MemorialDay
    {
        public MemorialDate Date { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public string ImagePath { set; get; }

        private ListViewItem lvi = null;

        private static List<Replacer> replaceString;

        public string HandledDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder(Description);
                foreach (Replacer rr in replaceString)
                    rr.Handle(sb, Date);
                return sb.ToString();
            }
        }

        public void Update()
        {
            if (lvi != null)
            {
                lvi.SubItems[0].Text = Date.Year.ToString();
                lvi.SubItems[1].Text = Date.Month.ToString();
                lvi.SubItems[2].Text = Date.Day.ToString();
                lvi.SubItems[3].Text = Title;
                lvi.SubItems[4].Text = Description;
                lvi.SubItems[5].Text = ImagePath;
            }
        }

        public ListViewItem GetListViewItem()
        {
            if (lvi == null)
                lvi = new ListViewItem(new string[]{
                Date.Year.ToString(),
                Date.Month.ToString(),
                Date.Day.ToString(),
                Title,
                Description,
                ImagePath});
            lvi.Tag = this;
            return lvi;
        }

        public MemorialDay(uint y, uint m, uint d, string title, string desc, string imgpath)
        {
            Date = new MemorialDate();
            Date.Year = y;
            Date.Month = m;
            Date.Day = d;
            Title = title;
            Description = desc;
            ImagePath = imgpath;
        }

        static MemorialDay()
        {
            replaceString = new List<Replacer>();

            replaceString.Add(new TodayDayReplacer());
        }
    }

    [Serializable]
    public class MemorialDate
    {
        public uint Year { set; get; }
        public uint Month { set; get; }
        public uint? Week { set; get; }
        public uint Day { set; get; }
        public override string ToString()
        {
            if(Week.HasValue)
                return Month.ToString("D4") + Week.Value.ToString("D1") + Day.ToString("D2");
            return Year.ToString("D4") + Month.ToString("D2") + Day.ToString("D2");
        }
    }
}
