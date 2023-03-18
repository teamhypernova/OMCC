namespace OMCCore.Globalization
{
    public interface ILanguageInfo
    {
        public string Id { get; }
        public string? GetString(string key);
    }
}
