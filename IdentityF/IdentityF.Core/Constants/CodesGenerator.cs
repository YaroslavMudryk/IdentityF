using IdentityF.Core.Options;

namespace IdentityF.Core.Constants
{
    public static class CodesGenerator
    {
        public const string ConfirmAccount = "ConfirmAccount";

        public static Dictionary<string, CodeOptions> Defaults = new Dictionary<string, CodeOptions>
        {
            { ConfirmAccount, new CodeOptions { Size = 64, IncludeLetters = true, IncludeNumbers = true } }
        };
    }
}
