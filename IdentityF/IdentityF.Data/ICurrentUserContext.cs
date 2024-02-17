namespace IdentityF.Data;

public interface ICurrentUserContext
{
    string GetIp();
    bool IsAdmin();
    string GetBearerToken();
    CurrentUser User { get; }
}
