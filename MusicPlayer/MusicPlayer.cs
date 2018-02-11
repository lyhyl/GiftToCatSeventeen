using System;
using System.Drawing;
using System.Windows.Forms;
using GiftToCatSeventeen.CatModel;
using GiftToCatSeventeen.CatPlugin;
using GiftToCatSeventeen.CatControl;

namespace MusicPlayer
{
    public class MusicPlayer : Plugin
    {
        private Image sicon, micon, licon;

        private StaticModel radio;

        private bool running = false;
        private bool shown = false;
        private Player player;

        public MusicPlayer(IServiceProvider Service)
            : base(Service)
        {
            Content.RootDirectory = "./Plugin/MusicPlayer/Data";
            licon = Image.FromFile("./Plugin/MusicPlayer/Data/Image/licon.png");
            micon = Image.FromFile("./Plugin/MusicPlayer/Data/Image/micon.png");
            sicon = Image.FromFile("./Plugin/MusicPlayer/Data/Image/sicon.png");

            player = new Player();
            player.Exit += new Player.HandleEvent(player_Exit);
        }

        void player_Exit()
        {
            shown = false;
        }

        public override StaticModel[] StaticModels
        {
            get { return null; }
        }

        public override DynamicModel[] DynamicModels
        {
            get { return null; }
        }

        public override void Update(UpdateParamater updatePara, CatController cc)
        {
            if (!shown)
            {
                player.Show();
                shown = true;
            }
            if (!running)
                running = true;
        }

        public override void UpdateBackground(UpdateParamater updatePara)
        {
        }

        public override void Exit()
        {
        }

        public override PluginState State
        {
            get
            {
                return shown ? PluginState.Active : (running ? PluginState.Background : PluginState.Inactive);
            }
        }

        public override ContextMenuStrip Menu { get { return null; } }

        #region Get Icon
        public override Image LargeIcon
        {
            get { return licon; }
        }
        public override Image MiddleIcon
        {
            get { return micon; }
        }
        public override Image SmallIcon
        {
            get { return sicon; }
        }
        #endregion
    }
}
