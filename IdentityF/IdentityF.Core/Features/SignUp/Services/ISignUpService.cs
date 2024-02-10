using IdentityF.Core.Features.SignUp.Dtos;

namespace IdentityF.Core.Features.SignUp.Services;

public interface ISignUpService
{
    Task SignUpAsync(SignUpDto signUpDto);
}