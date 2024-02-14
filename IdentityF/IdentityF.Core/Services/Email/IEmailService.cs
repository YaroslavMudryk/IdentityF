using IdentityF.Core.Services.Email.Dtos;

namespace IdentityF.Core.Services.Email
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailRequestDto emailRequestDto);
    }
}
