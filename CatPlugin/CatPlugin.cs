using System;
using System.Drawing;
using System.Windows.Forms;
using GiftToCatSeventeen.CatControl;
using GiftToCatSeventeen.CatModel;
using Microsoft.Xna.Framework.Content;

namespace GiftToCatSeventeen.CatPlugin
{
    public enum PluginState { Active, Inactive, Background }
    public abstract class Plugin
    {
        protected ContentManager Content;

        protected Plugin(IServiceProvider Services)
        {
            Content = new ContentManager(Services);
        }

        public delegate bool RequestActivationDelegate(Plugin plugin);
        public RequestActivationDelegate RequestActivation { set; get; }

        public abstract void Update(UpdateParamater updatePara, CatController cc);
        public abstract void UpdateBackground(UpdateParamater updatePara);
        public abstract void Exit();
        public abstract PluginState State { get; }

        public abstract StaticModel[] StaticModels { get; }
        public abstract DynamicModel[] DynamicModels { get; }

        public abstract Image LargeIcon { get; }
        public abstract Image MiddleIcon { get; }
        public abstract Image SmallIcon { get; }

        public abstract ContextMenuStrip Menu { get; }

        public virtual bool Autorun { get { return false; } }
        public virtual bool ShowIcon { get { return true; } }
    }
}
