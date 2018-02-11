using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using CalendarLayout;
using GiftToCatSeventeen.CatControl;
using GiftToCatSeventeen.CatModel;
using GiftToCatSeventeen.CatPlugin;
using Microsoft.Xna.Framework;

namespace Anniversary
{
    public class Anniversary : Plugin
    {
        private ContextMenuStrip menu = new ContextMenuStrip();
        private Image sicon, micon, licon;

        private AnniversaryCalendar calendar;
        private CalendarLayoutEngine layoutEngine;
        private MemorialDayCollection memorialDays;
        private TimeSpan checkDelay = new TimeSpan(0, 0, 3);
        private string MemorialDaysDataPath = "./Plugin/Anniversary/md.dat";

        private bool needGreet = false;
        private bool greetChecked = false;
        private bool greeting = false;
        private string greetingMsg;

        public Anniversary(IServiceProvider Services)
            : base(Services)
        {
            Content.RootDirectory = "./Plugin/Anniversary/Data";

            licon = Image.FromFile("./Plugin/Anniversary/Data/Image/licon.png");
            micon = Image.FromFile("./Plugin/Anniversary/Data/Image/micon.png");
            sicon = Image.FromFile("./Plugin/Anniversary/Data/Image/sicon.png");

            memorialDays = MemorialDayAccessor.Load(MemorialDaysDataPath);
            CreateMenu();
            InitializeLayoutEngine();
            InitializeCalendar();
        }

        private void InitializeCalendar()
        {
            calendar = new AnniversaryCalendar(memorialDays);
            calendar.Hide();
            calendar.SetEngine(layoutEngine);
        }

        private void CreateMenu()
        {
            menu.Items.Add("纪念日日历", null, new EventHandler(ShowCalendar));
        }

        public override void Exit()
        {
            MemorialDayAccessor.SaveMemorialDaysData(memorialDays, MemorialDaysDataPath);
        }

        private void InitializeLayoutEngine()
        {
            try
            {
                Assembly dll = Assembly.LoadFrom("./Plugin/Anniversary/CalendarFlatLayout.dll");
                Type leType = dll.GetType("CalendarLayout.CalendarFlatLayout", true);
                layoutEngine = Activator.CreateInstance(leType, null) as CalendarLayoutEngine;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Anniversary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //For single layout engine DLL
                //If has other vailed DLL , we should choose another one , not throw the exeption
                throw new ApplicationException("Layout Engine Error", e);
            }
        }

        private void ShowCalendar(object sender, EventArgs e)
        {
            calendar.Show((uint)DateTime.Now.Year, (uint)DateTime.Now.Month);
        }

        public override void Update(UpdateParamater updatePara, CatController cc)
        {
            if (!greeting)
            {
                greeting = true;
                greetingMsg = memorialDays.GetDay((uint)DateTime.Today.Month, (uint)DateTime.Today.Day).HandledDescription;
            }
            if (cc.Execute(cc.Commands["Greet"], new string[] { greetingMsg }))
                greeting = false;
        }

        public override void UpdateBackground(UpdateParamater updatePara)
        {
            if (!greetChecked && updatePara.GameTime.TotalGameTime > checkDelay)
            {
                needGreet = CheckGreeting();
                greetChecked = true;
            }

            if (needGreet)
                needGreet = !RequestActivation(this);
        }

        private bool CheckGreeting()
        {
            DateTime today = DateTime.Today;
            foreach (MemorialDay md in memorialDays)
                if (md.Date.Day == today.Day && md.Date.Month == today.Month)
                    return true;
            return false;
        }

        public override PluginState State { get { return greeting ? PluginState.Active : PluginState.Background; } }
        public override ContextMenuStrip Menu { get { return menu; } }
        public override StaticModel[] StaticModels { get { return null; } }
        public override DynamicModel[] DynamicModels { get { return null; } }
        public override bool Autorun { get { return true; } }
        public override Image LargeIcon { get { return licon; } }
        public override Image MiddleIcon { get { return micon; } }
        public override Image SmallIcon { get { return sicon; } }
    }
}
