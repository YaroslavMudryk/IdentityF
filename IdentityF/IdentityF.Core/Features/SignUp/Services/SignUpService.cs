using Extensions.Password;
using IdentityF.Core.Constants;
using IdentityF.Core.Exceptions;
using IdentityF.Core.Features.Shared.Managers.Services;
using IdentityF.Core.Features.SignUp.Dtos;
using IdentityF.Core.Helpers;
using IdentityF.Core.Options;
using IdentityF.Data;
using IdentityF.Data.Entities;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using YaMu.Helpers;

namespace IdentityF.Core.Features.SignUp.Services
{
    public class SignUpService : ISignUpService
    {
        private readonly IdentityFContext _db;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserManager _userManager;
        private readonly IRoleManager _roleManager;
        private readonly IdentityFOptions _options;
        public SignUpService(IdentityFContext db, IDateTimeProvider dateTimeProvider, IUserManager userManager, IRoleManager roleManager, IOptions<IdentityFOptions> options)
        {
            _db = db;
            _dateTimeProvider = dateTimeProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _options = options.Value;
        }

        public async Task SignUpAsync(SignUpDto signUpDto)
        {
            if (!Regex.IsMatch(signUpDto.Password, _options.Password.Regex))
                throw new PasswordRequirementsException(_options.Password.ErrorRegexMessages["en"]);

            var existUser = await _userManager.IsExistUsersAsync(user => user.Login == signUpDto.Login);
            if (existUser)
                throw new UserAlreadyRegisterdException("User with this login is already registered");

            if (!string.IsNullOrWhiteSpace(signUpDto.UserName) && await _userManager.IsExistUsersAsync(user => user.UserName == signUpDto.UserName))
                throw new UserAlreadyRegisterdException("User with this username is already registered");

            var now = _dateTimeProvider.UtcNow;

            var confirmAccount = Confirm.NewWithEmail(now, Generator.GetConfirmCode(_options.Codes[CodesGenerator.ConfirmAccount]));

            var userPassword = signUpDto.Password.GeneratePasswordHash();

            var newPassword = new Password
            {
                Hint = signUpDto.PasswordHint,
                PasswordHash = userPassword,
                IsActive = true
            };

            var role = await _roleManager.GetDefaultRoleAsync();

            var newUser = new User(signUpDto.FirstName, signUpDto.LastName, signUpDto.UserName, signUpDto.Login, userPassword)
            {
                Confirms = new List<Confirm> { confirmAccount },
                Passwords = new List<Password> { newPassword }
            };

            var newUserRole = new UserRole
            {
                User = newUser,
                RoleId = role.Id,
                IsActive = true,
                ActiveFrom = now,
                ActiveTo = now.AddYears(10),
            };

            await _db.Users.AddAsync(newUser);
            await _db.UserRoles.AddAsync(newUserRole);
            await _db.SaveChangesAsync();
        }
    }
}
