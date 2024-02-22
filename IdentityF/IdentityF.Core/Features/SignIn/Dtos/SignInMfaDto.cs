namespace IdentityF.Core.Features.SignIn.Dtos
{
    public class SignInMfaDto
    {
        public string Code { get; set; }
        public string SessionId { get; set; }
    }
}
