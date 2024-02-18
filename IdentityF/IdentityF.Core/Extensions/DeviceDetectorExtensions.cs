using Client = Extensions.DeviceDetector.Models.ClientInfo;
using IdentityF.Data.Entities.Internal;
using IdentityF.Data.Entities;

namespace IdentityF.Core.Extensions
{
    public static class DeviceDetectorExtensions
    {
        public static ClientInfo MapToClientInfo(this Client clientInfo)
        {
            if (clientInfo == null)
                return null;

            return new ClientInfo
            {
                Os = new OsInfo
                {
                    Name = clientInfo.OS.Name,
                    Platform = clientInfo.OS.Platform,
                    ShortName = clientInfo.OS.ShortName,
                    Version = clientInfo.OS.Version,
                },
                Browser = new BrowserInfo
                {
                    Version = clientInfo.Browser.Version,
                    Name = clientInfo.Browser.Name,
                    ShortName = clientInfo.Browser.ShortName,
                    Engine = clientInfo.Browser.Engine,
                    EngineVersion = clientInfo.Browser.EngineVersion,
                    Type = clientInfo.Browser.Type,
                },
                Device = new DeviceInfo
                {
                    Type = clientInfo.Device.Type,
                    Brand = clientInfo.Device.Brand,
                    Model = clientInfo.Device.Model,
                }
            };
        }

        public static Device MapToDevice(this ClientInfo clientInfo)
        {
            if (clientInfo == null)
                return null;

            return new Device
            {
                Brand = clientInfo.Device.Brand,
                Model = clientInfo.Device.Model,
                Type = clientInfo.Device.Type,
                VendorModel = clientInfo.Device.VendorModel,
                DeviceId = clientInfo.Device.DeviceId,
                Browser = clientInfo.Browser.Name,
                BrowserEngine = clientInfo.Browser.Engine,
                BrowserEngineVersion = clientInfo.Browser.EngineVersion,
                BrowserType = clientInfo.Browser.Type,
                BrowserVersion = clientInfo.Browser.Version,
                Os = clientInfo.Os.Name,
                OsPlatform = clientInfo.Os.Platform,
                OsShortName = clientInfo.Os.ShortName,
                OsVersion = clientInfo.Os.Version
            };
        }
    }
}
