using EDGW.Logging;
using OMCCore.Plugins;
using OMCCore.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OMCCore.Core
{
    public class Starter
    {
        public static Starter Instance { get; } = new Starter();
        private Starter() { }
        bool started = false;
        public void Start()
        {
            if (!started)
            {
                started = true;
                PreStart();
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
        void StartUI()
        {
            var mainw = new MainWindow();
            Application.Current.MainWindow = mainw;
            mainw.Show();
        }
        private void LoadPlugins()
        {
            Plugin.LoadPlugins();
        }

        private void StartLogger()
        {
            var fs = new FileStream("OMCL\\log.log", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fs.SetLength(0);
            Logger.SetWriter(new StreamWriter(fs));
        }

        void PrepareFiles()
        {
            Directory.CreateDirectory("OMCL");
        }
    }
}
