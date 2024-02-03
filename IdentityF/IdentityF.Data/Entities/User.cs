namespace IdentityF.Data.Entities
{
    public class User : BaseModel<int>
    {
        public User()
        {
            CanBeBlocked = true;
            BlockedUntil = null;
            FailedLoginAttempts = 0;
            Mfa = false;
            MfaSecretKey = null;
            IsConfirmed = false;
        }

        public User(string firstName, string lastName, string userName, string login, string passwordHash) : this()
        {
            
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


        public bool IsBlocked()
        {
            if (!CanBeBlocked)
                return false;

            bool isLocked;
            if (!BlockedUntil.HasValue)
                isLocked = false;
            else
            {
                if (BlockedUntil.Value > DateTime.Now)
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
