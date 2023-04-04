namespace OMCCore.Core.Game
{
    public class GameValidationInfo
    {
        public GameValidationInfo(bool isValidVersion, string? invalidMessage, string simpleInvalidMessage, bool isLaunchable)
        {
            IsValidVersion = isValidVersion;
            InvalidMessage = invalidMessage;
            IsLaunchable = isLaunchable;
            SimpleInvalidMessage = simpleInvalidMessage;
        }
        public GameValidationInfo(bool isValidVersion)
        {
            IsValidVersion = isValidVersion;
            InvalidMessage = null;
            SimpleInvalidMessage = null;
            IsLaunchable = IsValidVersion;
        }
        public GameValidationInfo(bool isValidVersion, string? invalidMessage, string? simpleInvalidMessage)
        {
            IsValidVersion = isValidVersion;
            InvalidMessage = invalidMessage;
            SimpleInvalidMessage = simpleInvalidMessage;
            IsLaunchable = IsValidVersion;
        }
        public GameValidationInfo(bool isValidVersion, string? invalidMessage) : this(isValidVersion, invalidMessage, invalidMessage)
        {

        }

        public override string ToString()
        {
            if (IsValidVersion)
            {
                return "True";
            }
            else
            {
                return $"False ({InvalidMessage})";
            }
        }
        public bool IsValidVersion { get; set; }
        public string? InvalidMessage { get; set; }
        public string? SimpleInvalidMessage { get; internal set; }
        public bool IsLaunchable { get; set; }
    }
}
