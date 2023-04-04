using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using OMCC.Plugins.GameManager;
using OMCC.Plugins.GameManager.Resources;
using OMCCore.Globalization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCC.Plugins.GameManager.Tests
{
    [TestClass()]
    public class GameManagerTests
    {
        [TestMethod()]
        public void GetVersionsTest()
        {
            var lanpack = new LanguagePack();
            lanpack.Languages.Add("zh_cn", DictionaryLanguageInfo.FromJson(JObject.Parse(Lan.zh_cn), "zh_cn"));
            Globalization.AddLanguagePack(lanpack);
            var mgr = GameManager.Current;
            if (mgr.Count == 0)
            {
                mgr.Add(new MinecraftDirectory() { DirectoryPath = "D:\\Server\\.minecraft" });
            }
            foreach (var dir in mgr.Values)
            {
                Debug.WriteLine(dir.Serialize());
                var vs = dir.GetVersions();
                Debug.WriteLine($"\t{vs.Length} versions.");
                foreach (var v in vs)
                {
                    var va = v.Validate();
                    Debug.WriteLine($"\t{v.LocalId}\n\tValidation(" + va + ")");
                    if (va.IsValidVersion)
                    {
                        Debug.WriteLine($"\t{v.JsonPath}");
                        Debug.WriteLine($"\t{v.JarPath}");
                        Debug.WriteLine($"\t{v.Name}");
                        Debug.WriteLine($"\t{v.Description}\n");
                    }
                }
            }

        }
        [TestMethod()]
        public void EnumTest()
        {
            var enume = GameManager.Current.Selected?.GetEnumerator();
            Debug.WriteLine(enume?.GetType().FullName);
        }
    }
}