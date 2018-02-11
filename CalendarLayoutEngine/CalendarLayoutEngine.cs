using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CalendarLayout
{
    public abstract class CalendarLayoutEngine
    {
        protected uint FirstDay, DaysCount;

        public virtual void Setup(uint day, uint daysCount)
        {
            FirstDay = day;
            DaysCount = daysCount;
        }

        public abstract Matrix YearPosition { get; }
        public abstract Matrix MonthPosition { get; }
        public abstract IEnumerable<Matrix> WeekdayPositions { get; }
        public abstract IEnumerable<Matrix> DayPositions { get; }

        public abstract int SelectDate(Point mouse);

        public abstract float FontSize { get; }
        public abstract int Width { get; }
        public abstract int Height { get; }

        public abstract Point SetDayBtnPosition { get; }
        public abstract Point PrevYearBtnPosition { get; }
        public abstract Point NextYearBtnPosition { get; }
        public abstract Point PrevMonthBtnPosition { get; }
        public abstract Point NextMonthBtnPosition { get; }
        public abstract Point CloseBtnPosition { get; }
    }
}
