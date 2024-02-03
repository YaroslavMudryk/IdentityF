namespace IdentityF.Data.Entities
{
    public class UserRole : BaseModel<int>
    {
        public DateTime ActiveFrom { set; get; }
        public DateTime? ActiveTo { set; get; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
