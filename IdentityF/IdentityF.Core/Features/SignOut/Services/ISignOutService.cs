namespace IdentityF.Core.Features.SignOut.Services
{
    public interface ISignOutService
    {
        Task<bool> LogoutAsync();
    }
}
