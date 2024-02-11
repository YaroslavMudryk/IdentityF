using IdentityF.Core.Features.Shared.Sms.Dtos;

namespace IdentityF.Core.Features.Shared.Sms.Services
{
    public interface ISmsService
    {
        Task<bool> SendSmsAsync(SmsRequestDto smsRequestDto);
    }
}
