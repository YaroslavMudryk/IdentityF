using IdentityF.Core.Features.Shared.Email.Dtos;

namespace IdentityF.Core.Features.Shared.Email.Services
{
    public class FakeEmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(EmailRequestDto emailRequest)
        {
            return await Task.FromResult(true);
        }
    }
}
