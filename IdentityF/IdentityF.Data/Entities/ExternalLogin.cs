namespace IdentityF.Data.Entities
{
    public class ExternalLogin : BaseModel<int>
    {
        public string Key { get; set; }
        public string Provider { get; set; }
        public bool IsActive { set; get; }
        public DateTime? DeactivatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
