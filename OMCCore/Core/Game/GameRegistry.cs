using EDGW.Logging;
using OMCCore.Core.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMCCore.Core.Game
{
    public class GameRegistry : Selector<IGameManager>
    {
        private GameRegistry() { }

        public static GameRegistry Current { get; } = new GameRegistry();
        protected override Logger Logger { get; } = new Logger("GameManager", nameof(GameRegistry));
    }
}
