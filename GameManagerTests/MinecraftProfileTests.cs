using Microsoft.VisualStudio.TestTools.UnitTesting;
using OMCC.Plugins.GameManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OMCC.Plugins.GameManager.Tests
{
    [TestClass()]
    public class MinecraftProfileTests
    {
        [TestMethod()]
        public void MinecraftProfileTest()
        {
            var app = new Application();
            new MinecraftLauncher((MinecraftVersion)(GameManager.Current.GetVersions().Where(x => ((MinecraftVersion)x).LocalId == "1.0").First())).Start();
        }

    }
}