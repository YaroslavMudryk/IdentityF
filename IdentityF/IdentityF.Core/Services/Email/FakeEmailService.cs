using IdentityF.Core.Services.Email.Dtos;

namespace IdentityF.Core.Services.Email
{
    public class FakeEmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(EmailRequestDto emailRequest)
        {
            return await Task.FromResult(true);
        }
    }
}
