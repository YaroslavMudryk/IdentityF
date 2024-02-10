﻿namespace IdentityF.Core.Exceptions
{
    public class UnauthorizedException : HttpResponseException
    {
        public UnauthorizedException() : this("Unauthorized")
        {
        }
        public UnauthorizedException(string message) : base(401, message)
        {
        }
    }
}
