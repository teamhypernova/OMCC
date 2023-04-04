using OMCC.Plugins.GameManager.Resources;
using OMCC.Plugins.GameManager.UI;
using OMCCore.Core;
using OMCCore.Core.Game;
using OMCCore.Model.Data;
using OMCCore.Plugins;
using OMCCore.UI;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace OMCC.Plugins.GameManager
{
    public class GameManager : ItemCollectionSelector<MinecraftDirectory>, IGameManager ,IUISupported
    {
        private GameManager()
        {
            
        }
        public static GameManager Current { get; } = new GameManager();

        public string Id => "plugin.official.gamemgr";
        public IReadOnlyList<MinecraftDirectory> Directories => Values;

        public IGameVersion? GetSelectedVersion() => Selected?.GetSelectedVersion();

        public IGameVersion[] GetVersions() => this.Selected?.GetVersions() ?? new MinecraftVersion[0];

        OPage IUISupported.CreatePage()
        {
            return new GameManagerPage();
        }
    }
}
