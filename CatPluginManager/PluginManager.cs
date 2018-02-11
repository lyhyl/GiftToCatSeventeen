using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using GiftToCatSeventeen.CatModel;
using GiftToCatSeventeen.CatPlugin;

using Microsoft.Xna.Framework;
using GiftToCatSeventeen.CatControl;

namespace GiftToCatSeventeen.CatPluginManager
{
    public class PluginManager
    {
        private List<Plugin> plugins = new List<Plugin>();
        public List<Plugin> Plugins { get { return plugins; } }

        private List<PluginButton> pluginButtons = null;
        public List<PluginButton> PluginButtons
        {
            get
            {
                if (pluginButtons != null)
                    return pluginButtons;
                pluginButtons = new List<PluginButton>(plugins.Count);
                foreach (Plugin plugin in plugins)
                    if (plugin.ShowIcon)
                        pluginButtons.Add(new PluginButton(this, plugin));
                return pluginButtons;
            }
        }

        private Plugin activePlugin;
        private List<Plugin> runningPlugins = new List<Plugin>();
        private Queue<Plugin> requestActivationPlugins = new Queue<Plugin>();

        public CatController CatController { set; get; }

        public PluginManager(IServiceProvider Services)
        {
            SearchPlugin(Services);
        }

        private void SearchPlugin(IServiceProvider Services)
        {
            DirectoryInfo dirinfo = new DirectoryInfo(System.Windows.Forms.Application.StartupPath + "/Plugin");
            FileSystemInfo[] fleinfo = dirinfo.GetFileSystemInfos();
            foreach (FileSystemInfo filesys in fleinfo)
                if (filesys is DirectoryInfo)
                    try
                    {
                        Assembly dll = Assembly.LoadFrom(filesys.FullName + "/" + filesys.Name + ".dll");
                        Type pluginType = dll.GetType(filesys.Name + "." + filesys.Name, true);
                        object[] para = { Services };
                        Plugin plugin = Activator.CreateInstance(pluginType, para) as Plugin;
                        if (plugin != null)
                            plugins.Add(plugin);
                        else
                            throw new Exception(string.Format("Convert Type {0} to Plugin Fail", pluginType));

                        plugin.RequestActivation = HandlePluginRequestActivation;
                        if (plugin.Autorun)
                            runningPlugins.Add(plugin);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Invailable Plugin \"" + filesys.Name + "\".\n" +
                            "\nError Message : " + e.Message + "\n" +
                            "\nThis plugin will be skipped.\n" +
                            (e.InnerException == null ? "" : "\nInner Message : " + e.InnerException.Message),
                            "Load Plugin Failed",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
        }

        public void Exit()
        {
            if (activePlugin != null)
                activePlugin.Exit();
            foreach (Plugin plugin in runningPlugins)
                plugin.Exit();
        }

        private bool HandlePluginRequestActivation(Plugin plugin)
        {
            if (activePlugin != null)
            {
                if (!(activePlugin == plugin || requestActivationPlugins.Contains(plugin)))
                    requestActivationPlugins.Enqueue(plugin);
                return false;
            }
            activePlugin = plugin;
            return true;
        }

        internal void RunPlugin(Plugin plugin)
        {
            if (plugin == null)
                throw new ArgumentException("Invaild Plugin. Cannot be null");
            if (activePlugin == null && activePlugin != plugin && !runningPlugins.Contains(plugin))
                activePlugin = plugin;
        }

        public void Update(GameTime gameTime)
        {
            UpdateParamater up = new UpdateParamater();
            up.GameTime = gameTime;

            Plugin active_remove_tmp = null;
            if (activePlugin != null)
            {
                activePlugin.Update(up, CatController);
                if (activePlugin.State != PluginState.Active)
                {
                    active_remove_tmp = activePlugin;
                    activePlugin = requestActivationPlugins.Count == 0 ? null : requestActivationPlugins.Dequeue();
                }
            }

            foreach (Plugin plugin in runningPlugins)
            {
                plugin.UpdateBackground(up);
                if (plugin.State == PluginState.Inactive)
                    runningPlugins.Remove(plugin);
            }

            if (active_remove_tmp != null)
                runningPlugins.Add(active_remove_tmp);
        }

        public List<StaticModel[]> StaticModels
        {
            get
            {
                List<StaticModel[]> mods = new List<StaticModel[]>(runningPlugins.Count);
                foreach (Plugin plugin in runningPlugins)
                    if (plugin.StaticModels != null)
                        mods.Add(plugin.StaticModels);
                if (activePlugin != null && activePlugin.StaticModels != null)
                    mods.Add(activePlugin.StaticModels);
                return mods;
            }
        }

        public List<DynamicModel[]> DynamicModels
        {
            get
            {
                List<DynamicModel[]> mods = new List<DynamicModel[]>(runningPlugins.Count);
                foreach (Plugin plugin in runningPlugins)
                    if (plugin.DynamicModels != null)
                    mods.Add(plugin.DynamicModels);
                if (activePlugin != null && activePlugin.DynamicModels != null)
                    mods.Add(activePlugin.DynamicModels);
                return mods;
            }
        }
    }
}
