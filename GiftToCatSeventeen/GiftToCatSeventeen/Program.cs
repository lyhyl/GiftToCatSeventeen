using System;
using System.Windows.Forms;
using GiftToCatSeventeen.CatPluginManager;
using GiftToCatSeventeen.Config;

namespace GiftToCatSeventeen
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThreadAttribute]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            ProgramSettings settings = new ProgramSettings();
            GiftToCatSeventeen game = new GiftToCatSeventeen(settings);
            DisplayForm displayForm = new DisplayForm(settings, game);
            PluginManager pluginManager = new PluginManager(game.Services);
            PluginForm pluginForm = new PluginForm(settings, pluginManager);
            game.PluginManager = pluginManager;
            game.DisplayForm = displayForm;
            displayForm.PluginForm = pluginForm;
            game.Run();
            settings.Save();
        }
    }
#endif
}

