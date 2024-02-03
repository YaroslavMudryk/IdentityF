namespace IdentityF.Data.Entities.Internal
{
    public class ClientInfo
    {
        public DeviceInfo Device { get; set; }
        public OsInfo Os { get; set; }
        public BrowserInfo Browser { get; set; }
        public string ClientId { get; set; }
    }

    public class BrowserInfo : BaseInfo
    {
        public string Type { get; set; }
        public string Engine { get; set; }
        public string EngineVersion { get; set; }
        public string ShortName { get; set; }
    }

    public class DeviceInfo
    {
        public string DeviceId { get; set; } // it can be like AndroidId or IDFV or IMEI or SerialNumber
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
    }

    public class OsInfo : BaseInfo
    {
        public string ShortName { get; set; }
        public string Platform { get; set; }
    }

    public class BaseInfo
    {
        public string Name { get; set; }
        public string Version { get; set; }
    }
}
