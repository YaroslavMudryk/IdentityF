using IdentityF.Core.Options;
using System.Text;

namespace IdentityF.Core.Helpers
{
    public class Generator
    {
        public static string GetConfirmCode(CodeOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);

            var codeBuilder = new StringBuilder();
            var random = new Random();

            string chars = "";
            if (options.IncludeNumbers)
                chars += "0123456789";
            if (options.IncludeLetters)
                chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            if (chars.Length == 0)
                throw new ArgumentException("At least one of IncludeNumbers or IncludeLetters must be true.");

            for (int i = 0; i < options.Size; i++)
            {
                codeBuilder.Append(chars[random.Next(chars.Length)]);
            }

            var code = codeBuilder.ToString();

            if (options.OnlyLowercase)
                code = code.ToLower();

            if (options.OnlyUppercase)
                code = code.ToUpper();

            return code;
        }

        public static string GetUserName()
        {
            var random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            var length = random.Next(4, 25);

            var usernameChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                usernameChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(usernameChars);
        }
    }
}
