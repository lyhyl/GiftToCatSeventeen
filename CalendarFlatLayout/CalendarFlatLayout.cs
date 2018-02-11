using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace CalendarLayout
{
    public class CalendarFlatLayout : CalendarLayoutEngine
    {
        private bool dirtyBuffer = true;
        private List<PointF> dayCenterBuffer = new List<PointF>();

        public override void Setup(uint day, uint daysCount)
        {
            base.Setup(day, daysCount);
            dirtyBuffer = true;
            dayCenterBuffer.Clear();
        }

        public override Matrix YearPosition
        {
            get
            {
                Matrix m = new Matrix();
                m.Translate(150, 37.5f);
                return m;
            }
        }

        public override Matrix MonthPosition
        {
            get
            {
                Matrix m = new Matrix();
                m.Translate(150, 56.25f);
                return m;
            }
        }

        public override IEnumerable<Matrix> WeekdayPositions
        {
            get
            {
                foreach (PointF cp in WeekdayCenter)
                {
                    Matrix m = new Matrix();
                    m.Translate(cp.X, cp.Y);
                    yield return m;
                }
            }
        }

        public override IEnumerable<Matrix> DayPositions
        {
            get
            {
                foreach (PointF cp in DayCenter)
                {
                    Matrix m = new Matrix();
                    m.Translate(cp.X, cp.Y);
                    yield return m;
                }
            }
        }

        public IEnumerable<PointF> WeekdayCenter
        {
            get
            {
                const float basex = 37.5f, basey = 93.75f, dx = 37.5f;
                for (uint c = 0; c < 7; ++c)
                    yield return new PointF(basex + c * dx, basey);
            }
        }

        private IEnumerable<PointF> DayCenter
        {
            get
            {
                if (dirtyBuffer)
                {
                    const float basex = 37.5f, basey = 131.25f, dx = 37.5f, dy = 37.5f;
                    uint week = 0;
                    for (uint c = FirstDay; c < DaysCount + FirstDay; ++c)
                    {
                        uint day = c % 7;
                        PointF cp = new PointF(basex + dx * day, basey + dy * week);
                        dayCenterBuffer.Add(cp);
                        yield return cp;
                        if (day == 6) ++week;
                    }
                    dirtyBuffer = false;
                }
                else
                    foreach (PointF cp in dayCenterBuffer)
                        yield return cp;
            }
        }

        public override int SelectDate(Point mouse)
        {
            int area = 15;
            int count = 1;
            foreach (PointF cp in dayCenterBuffer)
            {
                float dx = Math.Abs(mouse.X - cp.X);
                float dy = Math.Abs(mouse.Y - cp.Y);
                if (dx < area && dy < area)
                    return count;
                ++count;
            }
            return -1;
        }

        public override float FontSize { get { return 15; } }
        public override int Width { get { return 300; } }
        public override int Height { get { return 350; } }

        public override Point PrevYearBtnPosition
        {
            get { return new Point(150 - 45, 38); }
        }
        public override Point NextYearBtnPosition
        {
            get { return new Point(150 + 45, 38); }
        }
        public override Point PrevMonthBtnPosition
        {
            get { return new Point(150 - 35, 56); }
        }
        public override Point NextMonthBtnPosition
        {
            get { return new Point(150 + 35, 56); }
        }
        public override Point SetDayBtnPosition
        {
            get { return new Point(265, 15); }
        }
        public override Point CloseBtnPosition
        {
            get { return new Point(285, 15); }
        }
    }
}
