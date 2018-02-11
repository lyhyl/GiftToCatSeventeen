using System;
using System.Drawing;
using System.Windows.Forms;
using GiftToCatSeventeen.CatControl;
using GiftToCatSeventeen.CatModel;
using GiftToCatSeventeen.CatPlugin;

namespace IdleActivator
{
    public class IdleActivator : Plugin
    {
        PluginState state = PluginState.Background;
        TimeSpan lastActivationTime = TimeSpan.Zero;
        TimeSpan interval = new TimeSpan(0, 0, 10);
        bool executing = false;
        string currentCmd;

        public IdleActivator(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public override void Update(UpdateParamater updatePara, CatController cc)
        {
            if (!executing)
            {
                currentCmd = RandomCommand();
                executing = true;
            }
            if (cc.Execute(cc.Commands[currentCmd], null))
            {
                executing = false;
                state = PluginState.Background;
                lastActivationTime = updatePara.GameTime.TotalGameTime;
            }
        }

        Random rand = new Random();
        string[] cmds = new string[] { "WagTail", "Miaow" };
        private string RandomCommand()
        {
            return cmds[rand.Next(cmds.Length)];
        }

        public override void UpdateBackground(UpdateParamater updatePara)
        {
            if (updatePara.GameTime.TotalGameTime - lastActivationTime > interval)
                state = RequestActivation(this) ? PluginState.Active : PluginState.Background;
        }

        public override void Exit() { }
        public override PluginState State { get { return state; } }
        public override StaticModel[] StaticModels { get { return null; } }
        public override DynamicModel[] DynamicModels { get { return null; } }
        public override Image LargeIcon { get { return null; } }
        public override Image MiddleIcon { get { return null; } }
        public override Image SmallIcon { get { return null; } }
        public override ContextMenuStrip Menu { get { return null; } }
        public override bool Autorun { get { return true; } }
        public override bool ShowIcon { get { return false; } }
    }
}
