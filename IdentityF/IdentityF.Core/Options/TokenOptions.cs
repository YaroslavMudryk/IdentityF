namespace IdentityF.Core.Options
{
    public class TokenOptions
    {
        public bool UseSessionManager { get; set; } = true;
        public string Issuer { get; set; } = "IdentityF";
        public string Audience { get; set; } = "IdentityF Client";
        public string SecretKey { get; set; } = "0293fj2093fj3209fhg290gvj23rj032hf";
        public int LifetimeInMinutes { get; set; } = 120;
        public SessionManagerOptions SessionManager { get; set; } = new SessionManagerOptions();
    }
}
