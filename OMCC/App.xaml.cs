using EDGW.Logging;
using OMCCore.Plugins;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OMCC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Directory.CreateDirectory("OMCL");
            var fs = new FileStream("OMCL\\log.log", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            fs.SetLength(0);
            Logger.SetWriter(new StreamWriter(fs));
            Plugin.LoadPlugins();
        }
    }
}
