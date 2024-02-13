﻿namespace IdentityF.Data.Entities
{
    public class Token : BaseModel<Guid>
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiredAt { set; get; }
        public Guid SessionId { set; get; }
        public Session Session { set; get; }
    }
}