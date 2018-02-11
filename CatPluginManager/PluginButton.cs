using System;
using System.Drawing;
using System.Windows.Forms;

using GiftToCatSeventeen.CatPlugin;
using System.Drawing.Imaging;

namespace GiftToCatSeventeen.CatPluginManager
{
    public partial class PluginButton : Control
    {
        private Plugin TargetPlugin;
        private PluginManager PluginManager;

        public event EventHandler ShowMenu;
        public event EventHandler HideMenu;

        public PluginButton(PluginManager mgr, Plugin plugin)
        {
            InitializeComponent();

            PluginManager = mgr;
            TargetPlugin = plugin;

            SetStyle(ControlStyles.ResizeRedraw |
                ControlStyles.UserPaint |
                ControlStyles.SupportsTransparentBackColor,
                true);

            SmallSize = 24;
            MiddleSize = 48;
            LargeSize = 96;

            Width = Height = SmallSize;

            currentIcon = LargeIcon = plugin.LargeIcon;
            MiddleIcon = plugin.MiddleIcon;
            SmallIcon = plugin.SmallIcon;

            bound.X = bound.Y = 0;
            bound.Width = bound.Height = SmallSize;

            MouseClick += new MouseEventHandler(PluginButton_MouseClick);
            if (plugin.Menu != null)
                plugin.Menu.VisibleChanged += new EventHandler(Menu_VisibleChanged);
        }

        void Menu_VisibleChanged(object sender, EventArgs e)
        {
            if (!TargetPlugin.Menu.Visible)
                HideMenu(this, null);
        }

        void PluginButton_MouseClick(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    PluginManager.RunPlugin(TargetPlugin);
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    if (TargetPlugin.Menu != null)
                    {
                        TargetPlugin.Menu.Show(PointToScreen(e.Location));
                        ShowMenu(this, null);
                    }
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            float[][] cmele = 
            {
               new float[] {1,0,0,0,0}, //color 1, 0, 0
               new float[] {0,1,0,0,0}, //color 2, 0, 0
               new float[] {0,0,1,0,0}, //color 3, 0, 0
               new float[] {0,0,0,1,0}, //0, 0, 0,   0, 0
               new float[] {0,0,0,0,1}  //0, 0, 0,   0, 0
            };
            ColorMatrix cm = new ColorMatrix(cmele);
            ImageAttributes ia = new ImageAttributes();
            ia.SetColorMatrix(cm);
            if (currentIcon != null)
                pe.Graphics.DrawImage(currentIcon, bound, 0, 0, currentIcon.Width, currentIcon.Height, GraphicsUnit.Pixel, ia);

            base.OnPaint(pe);
        }

        public void ResizeIcon(int size)
        {
            Width = Height = bound.Width = bound.Height = size;
        }

        public void ShowLargeIcon()
        {
            ResizeIcon(LargeSize);
        }
        public void ShowMiddleIcon()
        {
            ResizeIcon(MiddleSize);
        }
        public void ShowSmallIcon()
        {
            ResizeIcon(SmallSize);
        }

        public void AlignedCenter(int x, int y)
        {
            Location = new Point(x - (bound.Width >> 1), y - (bound.Height >> 1));
        }

        private Rectangle bound = new Rectangle();
        private Image currentIcon;

        private Image licon;
        public Image LargeIcon { set { licon = value; } get { return licon; } }
        private Image micon;
        public Image MiddleIcon { set { micon = value; } get { return micon; } }
        private Image sicon;
        public Image SmallIcon { set { sicon = value; } get { return sicon; } }

        public static int SmallSize { set; get; }
        public static int MiddleSize { set; get; }
        public static int LargeSize { set; get; }
    }
}
