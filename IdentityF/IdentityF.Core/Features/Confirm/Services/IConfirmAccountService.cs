namespace IdentityF.Core.Features.Confirm.Services
{
    public interface IConfirmAccountService
    {
        Task<bool> ConfirmAccountAsync(string code, int userId);
        Task<bool> SendConfirmAsync(int userId);
    }
}
