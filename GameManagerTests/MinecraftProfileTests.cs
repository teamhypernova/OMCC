using Microsoft.VisualStudio.TestTools.UnitTesting;
using OMCC.Plugins.GameManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCC.Plugins.GameManager.Tests
{
    [TestClass()]
    public class MinecraftProfileTests
    {
        [TestMethod()]
        public void MinecraftProfileTest()
        {
            var prof = MinecraftProfile.Parse((MinecraftVersion)(GameManager.Current.GetVersions().Where(x => ((MinecraftVersion)x).LocalId == "1.14.4").First()));
        }

    }
}