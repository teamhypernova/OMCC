namespace OMCCore.Core.User
{
    public record UserInfo(
        string Username,
        string Uuid,
        string Usertype,
        string Token,
        string? Xuid
        );
}
