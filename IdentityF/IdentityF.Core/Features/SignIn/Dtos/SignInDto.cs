using IdentityF.Data.Entities.Internal;
using System.ComponentModel.DataAnnotations;

namespace IdentityF.Core.Features.SignIn.Dtos
{
    public class SignInDto
    {
        [Required, EmailAddress, StringLength(200, MinimumLength = 5)]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Lang { get; set; }
        public ClientInfo Device { get; set; }
        public AppSignInDto App { get; set; }
    }
}
