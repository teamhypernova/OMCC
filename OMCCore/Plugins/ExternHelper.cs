using System.IO;
using System.Runtime.Loader;

namespace OMCCore.Plugins
{
    public static class ExternHelper
    {
        public static void AppaDominLoadlibrary(Stream asm)
        {
            AssemblyLoadContext.Default.LoadFromStream(asm);
        }
    }
}
