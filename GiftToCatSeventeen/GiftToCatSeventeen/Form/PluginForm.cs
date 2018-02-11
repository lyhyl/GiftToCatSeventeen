using System;
using System.Drawing;
using EffectForm;
using GiftToCatSeventeen.CatPluginManager;
using GiftToCatSeventeen.Config;
using System.Windows.Forms;

namespace GiftToCatSeventeen
{
    public partial class PluginForm : EffectFormBase
    {
        private int alignedYCoord;
        private int[] alignedXCoords = { axc(1), axc(2), axc(3), axc(4), axc(5) };
        private static int axc(int c) { return c * 50; }

        private bool menuHolding = false;

        private PluginManager pluginManager;

        public PluginForm(ProgramSettings settings, PluginManager pluginMgr)
        {
            InitializeComponent();

            pluginManager = pluginMgr;
            SetupButtons();

            PrevPagePluginBtn.BackgroundImage = Image.FromFile("./Data/Image/PrevPlugin.png");
            NextPagePluginBtn.BackgroundImage = Image.FromFile("./Data/Image/NextPlugin.png");

            SizeChanged += new EventHandler(PluginForm_SizeChanged);
        }

        public void ShowPlugins()
        {
            HideDelayTimer.Enabled = false;
            Visible = true;
        }

        public void HidePlugins()
        {
            HideDelayTimer.Enabled = true;
        }

        private void DelayTimer_Tick(object sender, EventArgs e)
        {
            Visible = false;
            HideDelayTimer.Enabled = false;
        }

        private void SetupButtons()
        {
            foreach (PluginButton pluginButton in pluginManager.PluginButtons)
            {
                pluginButton.MouseEnter += new EventHandler(pluginButton_MouseEnter);
                pluginButton.MouseHover += new EventHandler(pluginButton_MouseHover);
                pluginButton.MouseLeave += new EventHandler(pluginButton_MouseLeave);
                pluginButton.ShowMenu += new EventHandler(pluginButton_ShowOrHideMenu);
                pluginButton.HideMenu += new EventHandler(pluginButton_ShowOrHideMenu);
                AddPluginButton(pluginButton);
            }
        }

        void PluginForm_SizeChanged(object sender, EventArgs e)
        {
            AlignedMiddle(Location.X, alignedYCoord);

            PrevPagePluginBtn.Location = new Point(PrevPagePluginBtn.Location.X,
                (Height - PrevPagePluginBtn.Height) >> 1);
            NextPagePluginBtn.Location = new Point(NextPagePluginBtn.Location.X,
                (Height - NextPagePluginBtn.Height) >> 1);

            int count = 0;
            foreach (PluginButton pluginButton in pluginManager.PluginButtons)
                pluginButton.AlignedCenter(alignedXCoords[count++ % 5], Height >> 1);
        }

        void pluginButton_ShowOrHideMenu(object sender, EventArgs e)
        {
            menuHolding = !menuHolding;
            if (!menuHolding)
            {
                ResetPluginButtons(sender as PluginButton);
                HideDelayTimer.Enabled = false;
            }
        }

        public void AddPluginButton(PluginButton pluginButton)
        {
            this.Controls.Add(pluginButton);
            //Prev & Next Button
            pluginButton.Location = new Point(alignedXCoords[(Controls.Count - 2) % 5], 0);
        }

        private void pluginButton_MouseEnter(object sender, EventArgs e)
        {
            Visible = true;
            HideDelayTimer.Enabled = false;
        }

        private void pluginButton_MouseHover(object sender, EventArgs e)
        {
            FocusPluginButton(sender as PluginButton);
        }

        private void pluginButton_MouseLeave(object sender, EventArgs e)
        {
            if (menuHolding) return;
            HideDelayTimer.Enabled = true;

            ResetPluginButtons(sender as PluginButton);
        }

        private void FocusPluginButton(PluginButton pluginButton)
        {
            if (pluginButton != null) pluginButton.ShowLargeIcon();
            Height = PluginButton.LargeSize;
        }

        private void ResetPluginButtons(PluginButton pluginButton)
        {
            if (pluginButton != null) pluginButton.ShowSmallIcon();
            Height = PluginButton.SmallSize;
        }

        public void AlignedMiddle(Point position)
        {
            alignedYCoord = position.Y;
            Location = new Point(position.X, position.Y - (Height >> 1));
        }

        public void AlignedMiddle(int x, int y)
        {
            alignedYCoord = y;
            Location = new Point(x, y - (Height >> 1));
        }
    }
}
