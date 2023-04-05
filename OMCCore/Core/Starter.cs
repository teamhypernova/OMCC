using EDGW.Logging;
using OMCCore.Core.Game;
using OMCCore.Core.User;
using OMCCore.Plugins;
using OMCCore.UI;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OMCCore.Core
{

    public class Starter
    {
        public string LauncherName => "OMCC";
        public string LauncherVersion => "deb0.0.1";
        public static Starter Instance { get; } = new Starter();
        private Starter() { }
        bool started = false;
        public void Start()
        {
            if (!started)
            {
                started = true;
                PreStart();
                Init();
                StartUI();
            }
            else
            {
                throw new ApplicationException("Cannot start twice.");
            }
        }
      
        void PreStart()
        {
            PrepareFiles();
            StartLogger();
            LoadPlugins();
        }

        void PrepareFiles()
        {
            Directory.CreateDirectory("OMCL");
        }
        private void StartLogger()
        {
            var fs = new FileStream("OMCL\\log.log", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fs.SetLength(0);
            Logger.SetWriter(new StreamWriter(fs));
        }

        private void LoadPlugins()
        {
            PluginRegistry.Current.LoadPlugins();
        }
        void Init()
        {
            PluginRegistry.Current.Init();
            UserRegistry.Current.Init();
            GameRegistry.Current.Init();
        }
        void StartUI()
        {
            var mainw = new MainWindow();
            Application.Current.MainWindow = mainw;
            mainw.Show();
        }
    }
}
