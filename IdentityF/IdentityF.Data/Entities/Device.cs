namespace IdentityF.Data.Entities
{
    public class Device : BaseModel<int>
    {
        public string DeviceHash { get; set; }
        public string DeviceId { get; set; } // it can be like AndroidId or IDFV or IMEI or SerialNumber
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Os { get; set; }
        public string OsVersion { get; set; }
        public string OsShortName { get; set; }
        public string OsPlatform { get; set; }
        public string Browser { get; set; }
        public string BrowserVersion { get; set; }
        public string BrowserType { get; set; }
        public string BrowserEngine { get; set; }
        public string BrowserEngineVersion { get; set; }

        public bool IsVerified { get; set; }
        public DateTime? VerifiedAt { get; set; }
        public Guid? VerifiedOnSessionId { get; set; }
    }
}
