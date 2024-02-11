using IdentityF.Core.Features.Shared.Email.Dtos;

namespace IdentityF.Core.Features.Shared.Email.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(EmailRequestDto emailRequestDto);
    }
}
