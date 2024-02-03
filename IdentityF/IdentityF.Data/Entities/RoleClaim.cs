namespace IdentityF.Data.Entities
{
    public class RoleClaim : BaseModel<int>
    {
        public DateTime ActiveFrom { set; get; }
        public DateTime? ActiveTo { set; get; }
        public bool IsActive { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public int ClaimId { get; set; }
        public Claim Claim { get; set; }
    }
}
