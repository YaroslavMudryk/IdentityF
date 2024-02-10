using System.ComponentModel.DataAnnotations;
namespace IdentityF.Core.Features.SignUp.Dtos;

public class SignUpDto
{
    [Required]
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [Required]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
    public string PasswordHint { get; set; }
    [StringLength(24, MinimumLength = 4)]
    public string UserName { get; set; }
}