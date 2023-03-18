using System.Collections.Generic;

namespace OMCCore.Globalization
{
    public class LanguagePack
    {
        public Dictionary<string, ILanguageInfo> Languages { get; } = new Dictionary<string, ILanguageInfo>();
    }
}
