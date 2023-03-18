namespace OMCCore.Core.User
{
    public interface IDelayedUser:IImmediateUser
    {
        public string NameDelayed { get; }
        public string UuidDelayed { get; }
        public string TokenDelayed { get; }
    }
}
