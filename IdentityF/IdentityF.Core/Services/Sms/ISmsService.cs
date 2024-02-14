using IdentityF.Core.Services.Sms.Dtos;

namespace IdentityF.Core.Services.Sms
{
    public interface ISmsService
    {
        Task<bool> SendSmsAsync(SmsRequestDto smsRequestDto);
    }
}
