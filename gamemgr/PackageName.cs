using System;

namespace OMCC.Plugins.GameManager
{
    public record PackageName(string Namespace, string Name, string Version,string? Native)
    {
        public override string ToString()
        {
            if (Native == null)
                return $"{Namespace}:{Name}:{Version}";
            else
                return $"{Namespace}:{Name}:{Version}:{Native}";
        }
        public string ToPath()
        {
            if (Native == null)
                return $"{Namespace.Replace(".", "/")}/{Name}/{Version}/{Name}-{Version}.jar";
            else
                return $"{Namespace.Replace(".", "/")}/{Name}/{Version}/{Name}-{Version}-{Native}.jar";
        }
        public static PackageName Parse(string package)
        {
            var strs = package.Split(':');
            if (strs.Length == 3)
            {
                return new PackageName(strs[0], strs[1], strs[2], null);
            }else if (strs.Length == 4)
            {
                return new PackageName(strs[0], strs[1], strs[2], strs[3]);
            }
            else
            {
                throw new FormatException("Invalid format of packageid:" + package + ".");
            }
        }
    }
}
