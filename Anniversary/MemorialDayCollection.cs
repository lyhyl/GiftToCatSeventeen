using System.Collections.Generic;
using System;

namespace Anniversary
{
    public class MemorialDayCollection : ICollection<MemorialDay>
    {
        private List<MemorialDay>[] mmDays = new List<MemorialDay>[12];

        public MemorialDayCollection()
        {
            for (int i = 0; i < 12; ++i)
                mmDays[i] = new List<MemorialDay>();
        }

        public List<MemorialDay> GetDateBy(uint m)
        {
            return mmDays[m - 1];
        }

        public void Add(uint y, uint m, uint d, string title, string desc, string imgpath)
        {
            MemorialDay md = new MemorialDay(y, m, d, title, desc, imgpath);
            mmDays[m - 1].Add(md);
        }

        public void Add(MemorialDay item)
        {
            if (item.Date.Month <= 12 && item.Date.Month > 0)
                mmDays[item.Date.Month - 1].Add(item);
        }

        public void Clear()
        {
            foreach (List<MemorialDay> list in mmDays)
                list.Clear();
        }

        public bool Contains(MemorialDay item)
        {
            foreach (List<MemorialDay> list in mmDays)
                if (list.Contains(item))
                    return true;
            return false;
        }

        public void CopyTo(MemorialDay[] array, int arrayIndex)
        {
            MemorialDay[] aDays = new MemorialDay[Count];
            int c = 0;
            foreach (List<MemorialDay> list in mmDays)
            {
                list.CopyTo(aDays, c);
                c += list.Count;
            }
        }

        public int Count
        {
            get
            {
                int c=0;
                foreach (List<MemorialDay> list in mmDays)
                    c += list.Count;
                return c;
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(MemorialDay item)
        {
            foreach (List<MemorialDay> list in mmDays)
                if (list.Contains(item))
                    return list.Remove(item);
            return false;
        }

        public IEnumerator<MemorialDay> GetEnumerator()
        {
            return new MemerialDayEnumerator(mmDays);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return new MemerialDayEnumerator(mmDays);
        }

        public MemorialDay GetDay(uint month, uint day)
        {
            --month;
            if (month > 11)
                return null;
            foreach (MemorialDay md in mmDays[month])
                if (md.Date.Day == day)
                    return md;
            return null;
        }

        private class MemerialDayEnumerator : IEnumerator<MemorialDay>
        {
            private List<MemorialDay>[] mmDays;
            private int m = 0, c = -1;

            public MemerialDayEnumerator(List<MemorialDay>[] mmdays)
            {
                mmDays = mmdays;
            }

            public MemorialDay Current
            {
                get { return mmDays[m][c]; }
            }

            public void Dispose()
            {
            }

            object System.Collections.IEnumerator.Current
            {
                get { return mmDays[m][c]; }
            }

            public bool MoveNext()
            {
                ++c;
                while (c >= mmDays[m].Count)
                {
                    c = 0;
                    ++m;
                    if (m > 11)
                        return false;
                }
                return true;
            }

            public void Reset()
            {
                m = 0;
                c = 0;
            }
        }
    }
}
