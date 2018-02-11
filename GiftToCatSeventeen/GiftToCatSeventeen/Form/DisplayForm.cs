using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using EffectForm;
using GiftToCatSeventeen.Config;

namespace GiftToCatSeventeen
{
    public partial class DisplayForm : EffectFormBase
    {
        private ProgramSettings programSettings;
        private GiftToCatSeventeen xnaForm;
        private PluginForm pluginForm;
        private Bitmap renderBitmap;

        private Point mouseDownPosition = new Point();
        private bool mouseIsDown = false;

        public PluginForm PluginForm
        {
            set
            {
                pluginForm = value;

                pluginForm.Show(this);
                pluginForm.Visible = false;
                pluginForm.AlignedMiddle(Location);
            }
        }
        public ProgramSettings Settings
        {
            set
            {
                programSettings = value;
                renderBitmap.Dispose();
                renderBitmap = new Bitmap(programSettings.Width, programSettings.Height,
                    PixelFormat.Format32bppArgb);
            }
            get
            {
                return programSettings;
            }
        }

        public DisplayForm(ProgramSettings settings, GiftToCatSeventeen XNAForm)
        {
            InitializeComponent();

            UseBackgroundImage = true;
            Alpha = 0;
            BlendAlphaInSpeed *= 0.5;

            xnaForm = XNAForm;
            programSettings = settings;

            SetupProperty();
            RegisterEvent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            RefreshTimer.Enabled = true;
        }

        private void SetupProperty()
        {
            Width = programSettings.Width;
            Height = programSettings.Height;

            renderBitmap = new Bitmap(programSettings.Width, programSettings.Height,
                PixelFormat.Format32bppArgb);
        }

        private void RegisterEvent()
        {
            KeyDown += new KeyEventHandler(DisplayForm_KeyDown);

            MouseDown += new MouseEventHandler(DisplayForm_MouseDown);
            MouseMove+=new MouseEventHandler(DisplayForm_MouseMove);
            MouseUp += new MouseEventHandler(DisplayForm_MouseUp);
            MouseHover += new EventHandler(DisplayForm_MouseHover);
            MouseLeave += new EventHandler(DisplayForm_MouseLeave);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            if (MessageBox.Show("ÕæµÄÂð£¿ß÷£¿", "ÍË³ö", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                xnaForm.Exit();
            }
        }

        void Refresh_Tick(object sender, EventArgs e)
        {
            xnaForm.RenderToBitmap(renderBitmap);
            SetBits(renderBitmap);
        }

        #region Control Event

        void DisplayForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        void DisplayForm_MouseDown(object sender, MouseEventArgs e)
        {
            mouseIsDown = true;
            mouseDownPosition = e.Location;
        }

        void DisplayForm_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        void DisplayForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                Point vector = new Point(
                    e.X - mouseDownPosition.X + Location.X,
                    e.Y - mouseDownPosition.Y + Location.Y);

                Location = vector;

                pluginForm.AlignedMiddle(vector);
            }
        }

        void DisplayForm_MouseHover(object sender, EventArgs e)
        {
            pluginForm.ShowPlugins();
        }

        void DisplayForm_MouseLeave(object sender, EventArgs e)
        {
            pluginForm.HidePlugins();
        }

        #endregion

        private void MuteMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MuteMToolStripMenuItem.Checked = !MuteMToolStripMenuItem.Checked;
            Microsoft.Xna.Framework.Audio.SoundEffect.MasterVolume = MuteMToolStripMenuItem.Checked ? 0 : 1;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}