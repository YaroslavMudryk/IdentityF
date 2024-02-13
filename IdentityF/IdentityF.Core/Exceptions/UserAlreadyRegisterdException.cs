﻿using IdentityF.Data.Entities;

namespace IdentityF.Core.Exceptions
{
    public class UserAlreadyRegisterdException : HttpResponseException
    {
        public UserAlreadyRegisterdException() : base(400, "User with this login is already registered")
        {

        }

        public UserAlreadyRegisterdException(string error) : base(400, error)
        {
        }
    }
}
