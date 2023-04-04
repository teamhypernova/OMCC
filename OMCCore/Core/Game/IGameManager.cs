using OMCCore.Model.Data;

namespace OMCCore.Core.Game
{
    public interface IGameManager : IId
    {
        public IGameVersion[] GetVersions();
        public IGameVersion?
            GetSelectedVersion();
    }
}
