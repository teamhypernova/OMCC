namespace OMCC.Plugins.GameManager
{
    public interface IDownloadInfo
    {
        public long Size { get; }
        public string Url { get; }
        public string Path { get; }
        public string? Sha1 { get; }
    }
}
