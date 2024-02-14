using IdentityF.Core.Constants;

namespace IdentityF.Core.Options
{
    public class IdentityFOptions
    {
        public string Name { get; set; } = "IdentityF";
        public string ConnectionString { get; set; } = "Data Source=IdentityDb.db3";
        public PasswordOptions Password { get; set; } = new PasswordOptions();
        public TokenOptions Token { get; set; } = new TokenOptions();
        public Dictionary<string, EndpointOptions> Endpoints { get; set; } = HttpEndpoints.Default;
        public Dictionary<string, CodeOptions> Codes { get; set; } = CodeConfigs.Defaults;
    }
}
