using System.Text.Json.Serialization;

namespace IdentityF.Data.Entities
{
    public class User : BaseModel<int>
    {
        public User()
        {
            IsConfirmed = true;
            FailedLoginAttempts = 0;
            CanBeBlocked = true;
            BlockedUntil = null;
            Mfa = false;
            MfaSecretKey = null;
        }

        public User(string firstName, string lastName, string userName, string login, string passwordHash) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            UserName = userName;
            Login = login;
            PasswordHash = passwordHash;
            Email = login;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Image { get; set; }

        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public bool CanBeBlocked { get; set; }
        public DateTime? BlockedUntil { get; set; }
        public int FailedLoginAttempts { get; set; }

        public bool Mfa { get; set; }
        public string MfaSecretKey { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsConfirmed { get; set; }

        [JsonIgnore]
        public List<Password> Passwords { get; set; }
        [JsonIgnore]
        public List<Block> Blocks { get; set; }
        [JsonIgnore]
        public List<Mfa> Mfas { get; set; }
        [JsonIgnore]
        public List<Confirm> Confirms { get; set; }
        [JsonIgnore]
        public List<Contact> Contacts { get; set; }
        [JsonIgnore]
        public List<UserRole> UserRoles { get; set; }
        [JsonIgnore]
        public List<ExternalLogin> ExternalLogins { get; set; }
        [JsonIgnore]
        public List<Qr> Qrs { get; set; }
        [JsonIgnore]
        public List<LoginAttempt> LoginAttempts { get; set; }
        [JsonIgnore]
        public List<Session> Sessions { get; set; }

        public bool IsBlocked(DateTime dateTime)
        {
            if (!CanBeBlocked)
                return false;

            bool isLocked;
            if (!BlockedUntil.HasValue)
                isLocked = false;
            else
            {
                if (BlockedUntil.Value > dateTime)
                    isLocked = true;
                else
                {
                    BlockedUntil = null;
                    isLocked = false;
                }
            }
            return isLocked;
        }
    }
}
