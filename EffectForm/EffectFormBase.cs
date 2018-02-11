using System;
using System.Drawing;
using System.Windows.Forms;

namespace EffectForm
{
    public class EffectFormBase : Form
    {
        private Timer BlendTimer;

        protected bool Showing { get; set; }

        private byte alpha = 255;
        protected double Alpha
        {
            set
            {
                if (value > 1) value = 1;
                if (value < 0) value = 0;
                if (UseBackgroundImage)
                    alpha = (byte)(value * 255.0);
                else
                    Opacity = value;
            }
            get
            {
                return UseBackgroundImage ? (double)alpha / 255.0 : Opacity;
            }
        }

        private bool useBackgroundImage;
        protected bool UseBackgroundImage
        {
            get { return useBackgroundImage; }
            set
            {
                useBackgroundImage = value;
                if (value) Opacity = 1;
            }
        }

        public double BlendAlphaInSpeed { set; get; }
        public double BlendAlphaOutSpeed { set; get; }

        public int BlendInTime { get { return (int)(BlendTimer.Interval * BlendAlphaInSpeed); } }
        public int BlendOutTime { get { return (int)(BlendTimer.Interval * BlendAlphaOutSpeed); } }

        public EffectFormBase()
        {
            Showing = true;
            UseBackgroundImage = false;
            BlendAlphaInSpeed = 0.1;
            BlendAlphaOutSpeed = -0.05;
            Alpha = 0;

            BlendTimer = new Timer();
            BlendTimer.Interval = 10;
            BlendTimer.Tick += new EventHandler(BlendTimer_Tick);
        }

        #region Fade

        protected override void SetVisibleCore(bool value)
        {
            Showing = value;
            if (Showing)
            {
                base.SetVisibleCore(true);
                BlendTimer.Enabled = Alpha < 1;
            }
            else
            {
                BlendTimer.Enabled = Alpha > 0;
            }
        }

        private void BlendTimer_Tick(object sender, EventArgs e)
        {
            Alpha += Showing ? BlendAlphaInSpeed : BlendAlphaOutSpeed;

            if (Alpha == 1)
            {
                BlendTimer.Enabled = false;
            }
            else if (Alpha == 0)
            {
                BlendTimer.Enabled = false;
                base.SetVisibleCore(false);
            }
        }

        #endregion

        #region Blend Form Alpha

        protected override void OnHandleCreated(EventArgs e)
        {
            InitializeStyles();
            base.OnHandleCreated(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cParms = base.CreateParams;
                cParms.ExStyle |= 0x00080000;// WS_EX_LAYERED
                return cParms;
            }
        }

        private void InitializeStyles()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            UpdateStyles();
        }

        public void SetBits(Bitmap bitmap)
        {
            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32AlphaBlend.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32AlphaBlend.CreateCompatibleDC(screenDC);

            try
            {
                Win32AlphaBlend.Point topLoc = new Win32AlphaBlend.Point(Left, Top);
                Win32AlphaBlend.Size bitMapSize = new Win32AlphaBlend.Size(bitmap.Width, bitmap.Height);
                Win32AlphaBlend.BLENDFUNCTION blendFunc = new Win32AlphaBlend.BLENDFUNCTION();
                Win32AlphaBlend.Point srcLoc = new Win32AlphaBlend.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32AlphaBlend.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32AlphaBlend.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = alpha;
                blendFunc.AlphaFormat = Win32AlphaBlend.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32AlphaBlend.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32AlphaBlend.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32AlphaBlend.SelectObject(memDc, oldBits);
                    Win32AlphaBlend.DeleteObject(hBitmap);
                }
                Win32AlphaBlend.ReleaseDC(IntPtr.Zero, screenDC);
                Win32AlphaBlend.DeleteDC(memDc);
            }
        }

        #endregion
    }
}
