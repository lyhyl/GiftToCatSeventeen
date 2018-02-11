using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using CalendarLayout;
using EffectForm;

namespace Anniversary
{
    public partial class AnniversaryCalendar : EffectFormBase
    {
        public uint CurrentYear { private set; get; }
        public uint CurrentMonth { private set; get; }

        private uint[] DaysOfMonth = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private bool isLeapYear = false;

        private Font font;
        private FontFamily fontFamily;
        private PointF orgPos = new PointF(0, 0);

        private MemorialDayCollection memorialDays;
        private CalendarLayoutEngine layoutEngine;
        private StringFormat stringFormat;

        private Point mousePosition = Point.Empty;
        private bool mouseIsDown = false;

        private MemorialDayViewer updateMDayForm;

        private int showingDay = -1;

        public AnniversaryCalendar(MemorialDayCollection mdays)
        {
            InitializeComponent();

            //MDayDescTip.IsBalloon = true;
            MDayDescTip.UseFading = true;
            MDayDescTip.UseAnimation = true;

            updateMDayForm = new MemorialDayViewer();
            updateMDayForm.MemorialDays = mdays;
            memorialDays = mdays;

            CurrentYear = (uint)DateTime.Today.Year;
            CurrentMonth = (uint)DateTime.Today.Month;

            stringFormat = new StringFormat();
            stringFormat.LineAlignment = StringAlignment.Center;
            stringFormat.Alignment = StringAlignment.Center;

            MouseDown += new MouseEventHandler(AnniversaryCalendar_MouseDown);
            MouseMove += new MouseEventHandler(AnniversaryCalendar_MouseMove);
            MouseUp += new MouseEventHandler(AnniversaryCalendar_MouseUp);
        }

        private void ShowDateDescription(MouseEventArgs e)
        {
            int no = layoutEngine.SelectDate(e.Location);
            if (no != -1 && showingDay != no)
                foreach (MemorialDay md in memorialDays.GetDateBy(CurrentMonth))
                    if (md.Date.Month == CurrentMonth && md.Date.Day == no)
                    {
                        ShowTip(e, md);
                        break;
                    }
            showingDay = no;
        }

        private void ShowTip(MouseEventArgs e, MemorialDay md)
        {
            MDayDescTip.ToolTipTitle = md.Title;
            string msg = md.HandledDescription;
            MDayDescTip.Show(msg, this, e.X, e.Y, msg.Length << 9);
        }

        public void Show(uint year, uint month)
        {
            if (layoutEngine == null)
                throw new ApplicationException("layout engine is null in Calendar.Show");

            CurrentYear = year;
            CurrentMonth = month;

            SetIsLeapYear();
            Show();
            UpdateData();
        }

        public void SetEngine(CalendarLayoutEngine engine)
        {
            if (layoutEngine == engine)
                return;

            layoutEngine = engine;
            font = new Font("Arial", engine.FontSize);
            fontFamily = new FontFamily("Arial");
            BackColor = Color.FromArgb(unchecked((int)0xffdee6ce));

            PrevMonthBtn.Location = engine.PrevMonthBtnPosition
                - new Size(PrevMonthBtn.Width >> 1, PrevMonthBtn.Height >> 1);
            NextMonthBtn.Location = engine.NextMonthBtnPosition
                - new Size(NextMonthBtn.Width >> 1, NextMonthBtn.Height >> 1);
            PrevYearBtn.Location = engine.PrevYearBtnPosition
                - new Size(PrevYearBtn.Width >> 1, PrevYearBtn.Height >> 1);
            NextYearBtn.Location = engine.NextYearBtnPosition
                - new Size(NextYearBtn.Width >> 1, NextYearBtn.Height >> 1);
            SetDayBtn.Location = engine.SetDayBtnPosition
                - new Size(SetDayBtn.Width >> 1, SetDayBtn.Height >> 1);
            CloseBtn.Location = engine.CloseBtnPosition
                - new Size(CloseBtn.Width >> 1, CloseBtn.Height >> 1);
        }

        private void SetIsLeapYear()
        {
            if (CurrentYear % 400 == 0)
                isLeapYear = true;
            else if (CurrentYear % 100 == 0)
                isLeapYear = false;
            else if (CurrentYear % 4 == 0)
                isLeapYear = true;
            else
                isLeapYear = false;
        }

        //Zeller's congruence
        private uint CalculateFirstDay()
        {
            //uint d = 1;
            int cy = (int)CurrentYear;
            int m = (int)CurrentMonth;
            if (m == 1 | m == 2)
            {
                cy--;
                m += 12;
            }
            int y = cy % 100;
            int c = cy / 100;
            //uint w = y + y / 4 + c / 4 - (c << 1) + (26 * (m + 1)) / 10 + d - 1;
            int w = y + (y >> 2) + (c >> 2) - (c << 1) + (13 * (m + 1)) / 5;
            return (uint)((w % 7 + 7) % 7);
        }

        private void UpdateData()
        {
            layoutEngine.Setup(CalculateFirstDay(), DaysOfMonth[CurrentMonth - 1] + (isLeapYear ? 1u : 0u));

            Height = layoutEngine.Height;
            Width = layoutEngine.Width;

            Refresh();
        }

        #region Override

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            e.Graphics.Transform = layoutEngine.YearPosition;
            e.Graphics.DrawString(CurrentYear.ToString(), font, Brushes.DarkBlue, orgPos, stringFormat);

            e.Graphics.Transform = layoutEngine.MonthPosition;
            e.Graphics.DrawString(CurrentMonth.ToString(), font, Brushes.DarkBlue, orgPos, stringFormat);

            string[] weekdayStr = { "Su.", "Mo.", "Tu.", "We.", "Th.", "Fr.", "Sa." };
            uint wd = 0;
            using (Brush brush = new SolidBrush(Color.FromArgb(unchecked((int)0xff73bd8c))))
            {
                foreach (Matrix tm in layoutEngine.WeekdayPositions)
                {
                    e.Graphics.Transform = tm;
                    e.Graphics.DrawString(weekdayStr[wd++], font, brush, orgPos, stringFormat);
                }
            }

            uint days = 1;
            GraphicsPath gp = new GraphicsPath();
            using (Brush brush = new SolidBrush(Color.FromArgb(unchecked((int)0xffffe6d6))))
            {
                Pen pen = new Pen(brush, 4);
                foreach (Matrix tm in layoutEngine.DayPositions)
                {
                    e.Graphics.Transform = tm;
                    gp.AddString(days.ToString(), fontFamily, (int)FontStyle.Regular, layoutEngine.FontSize, orgPos, stringFormat);
                    if (memorialDays.GetDay(CurrentMonth, days) != null)
                        e.Graphics.DrawPath(pen, gp);
                    e.Graphics.FillPath(Brushes.OrangeRed, gp);
                    gp.Reset();
                    ++days;
                }
                pen.Dispose();
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            Hide();
        }

        #endregion

        #region Event

        #region Form Event

        private void AnniversaryCalendar_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void AnniversaryCalendar_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                Point v = e.Location - (Size)mousePosition;
                Location += (Size)v;
            }

            if (e.X < Width && e.Y < Height)
                ShowDateDescription(e);
        }

        private void AnniversaryCalendar_MouseDown(object sender, MouseEventArgs e)
        {
            mouseIsDown = true;
            mousePosition = e.Location;
        }

        #endregion

        #region Control Event

        private void SetDayBtn_Click(object sender, EventArgs e)
        {
            updateMDayForm.Show();
        }

        private void PrevYearBtn_Click(object sender, EventArgs e)
        {
            --CurrentYear;
            SetIsLeapYear();
            UpdateData();
        }

        private void NextYearBtn_Click(object sender, EventArgs e)
        {
            ++CurrentYear;
            SetIsLeapYear();
            UpdateData();
        }

        private void PrevMonthBtn_Click(object sender, EventArgs e)
        {
            --CurrentMonth;
            if (CurrentMonth < 1)
            {
                --CurrentYear;
                CurrentMonth = 12;
            }
            SetIsLeapYear();
            UpdateData();
        }

        private void NextMonthBtn_Click(object sender, EventArgs e)
        {
            ++CurrentMonth;
            if (CurrentMonth > 12)
            {
                ++CurrentYear;
                CurrentMonth = 1;
            }
            SetIsLeapYear();
            UpdateData();
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        #endregion

        #endregion
    }
}
