﻿using System.Text.Json.Serialization;

namespace IdentityF.Data.Entities
{
    public class Role : BaseModel<int>
    {
        public Role()
        {

        }

        public Role(string name)
        {
            Name = name;
            NameNormalized = name.ToUpper();
        }

        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public string NameNormalized { get; set; }
        [JsonIgnore]
        public List<UserRole> UserRoles { get; set; }
        [JsonIgnore]
        public List<RoleClaim> RoleClaims { get; set; }
    }
}
