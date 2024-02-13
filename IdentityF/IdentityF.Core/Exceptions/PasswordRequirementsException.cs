namespace IdentityF.Core.Exceptions
{
    public class PasswordRequirementsException : HttpResponseException
    {
        public PasswordRequirementsException() : this("The password does not meet the requirements")
        {

        }

        public PasswordRequirementsException(string error) : base(400, error)
        {

        }
    }
}
