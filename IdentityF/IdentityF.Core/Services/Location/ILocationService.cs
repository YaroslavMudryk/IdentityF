using IdentityF.Data.Entities;

namespace IdentityF.Core.Services.Location
{
    public interface ILocationService
    {
        Task<LocationInfo> GetIpInfoAsync(string ip);
    }
}
