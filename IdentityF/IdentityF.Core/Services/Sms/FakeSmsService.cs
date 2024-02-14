using IdentityF.Core.Services.Sms.Dtos;

namespace IdentityF.Core.Services.Sms
{
    public class FakeSmsService : ISmsService
    {
        public async Task<bool> SendSmsAsync(SmsRequestDto smsRequestDto)
        {
            return await Task.FromResult(true);
        }
    }
}
