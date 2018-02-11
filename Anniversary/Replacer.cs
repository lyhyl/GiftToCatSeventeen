using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Anniversary
{
    internal abstract class Replacer
    {
        public abstract void Handle(StringBuilder sb, MemorialDate date);
    }

    //today ? that day ?
    internal class TodayDayReplacer : Replacer
    {
        public override void Handle(StringBuilder sb, MemorialDate date)
        {
            string str =
                DateTime.Today.Month == date.Month && DateTime.Today.Day == date.Day ? "今日" : "个日";
            sb.Replace("[tt]", str);
        }
    }
}
