using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using EffectForm;

namespace GiftToCatSeventeen.CatControl
{
    public partial class GreetingForm : EffectFormBase
    {
        PrivateFontCollection extraFonts = null;
        private string greetingMessage;
        private Timer hideTimer = new Timer();

        public enum ShowSide { Left, Right, Top, Bottom };
        public ShowSide GreetingShowSide { set; get; }

        public GreetingForm()
        {
            InitializeComponent();

            Font = CreateExtraFont("FZMiaow", 16);
            hideTimer.Tick += new EventHandler(hideTimer_Tick);
        }

        public void ShowGreeting(string msg, Point pos, ShowSide ss)
        {
            greetingMessage = msg;
            GreetingShowSide = ss;
            hideTimer.Interval = (msg.Length << 8) + BlendInTime;
            hideTimer.Enabled = true;
            Location = pos - new Size(Width >> 1, Height);
            Visible = true;
        }

        private void hideTimer_Tick(object sender, EventArgs e)
        {
            hideTimer.Enabled = false;
            Visible = false;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            int cornerRadius = 10;
            int arrLen = 20;
            SizeF strSize = e.Graphics.MeasureString(greetingMessage, Font);
            int width=(int)Math.Ceiling(strSize.Width) + (cornerRadius << 1);
            int height=(int)Math.Ceiling(strSize.Height) + (cornerRadius << 1);
            Size = new Size(width, height + arrLen);
            e.Graphics.FillPath(Brushes.LightBlue, CreateRoundedRectanglePath(0, 0, width, height, cornerRadius));
            e.Graphics.FillPolygon(Brushes.LightBlue,
                new Point[] { new Point(width >> 1, height),
                    new Point(width + arrLen >> 1, height),
                    new Point(width >> 1, height + arrLen) });
            e.Graphics.DrawString(greetingMessage, Font, Brushes.Black, cornerRadius, cornerRadius);
        }

        private Font CreateExtraFont(string name, float size)
        {
            if (extraFonts != null)
                return new Font(extraFonts.Families[extraFonts.Families.Length - 1], size);
            extraFonts = new PrivateFontCollection();
            extraFonts.AddFontFile("./Data/Font/" + name + ".ttf");
            return new Font(extraFonts.Families[0], size);
        }
        private Font GetExtraFont(int id, float size)
        {
            return new Font(extraFonts.Families[id], size);
        }

        private GraphicsPath CreateRoundedRectanglePath(int x, int y, int width, int height, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            int drad = cornerRadius << 1;
            roundedRect.AddArc(x, y, drad, drad, 180, 90);
            roundedRect.AddLine(x + cornerRadius, y, x + width - drad, y);
            roundedRect.AddArc(x + width - drad, y, drad, drad, 270, 90);
            roundedRect.AddLine(x + width, y + cornerRadius, x + width, y + height - cornerRadius);
            roundedRect.AddArc(x + width - drad, y + height - drad, drad, drad, 0, 90);
            roundedRect.AddLine(x + width - cornerRadius, y + height, x + cornerRadius, y + height);
            roundedRect.AddArc(x, y + height - drad, drad, drad, 90, 90);
            roundedRect.AddLine(x, y + height - cornerRadius, x, y + cornerRadius);
            roundedRect.CloseFigure();
            return roundedRect;
        }
    }
}
