using IdentityF.Core.Features.Shared.Sms.Dtos;

namespace IdentityF.Core.Features.Shared.Sms.Services
{
    public class FakeSmsService : ISmsService
    {
        public async Task<bool> SendSmsAsync(SmsRequestDto smsRequestDto)
        {
            return await Task.FromResult(true);
        }
    }
}
