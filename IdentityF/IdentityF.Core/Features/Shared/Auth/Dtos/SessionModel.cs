namespace IdentityF.Core.Features.Shared.Auth.Dtos
{
    public class SessionModel
    {
        public Guid Id { get; set; }
        public int UserId { get; set; }

        public List<TokenModel> Tokens { get; set; }
    }
}
