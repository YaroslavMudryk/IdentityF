namespace IdentityF.Data.Entities
{
    public class AppClaim : BaseModel<int>
    {
        public DateTime ActiveFrom { set; get; }
        public DateTime? ActiveTo { set; get; }
        public bool IsActive { get; set; }
        public int ClaimId { get; set; }
        public Claim Claim { get; set; }
        public int AppId { get; set; }
        public App App { get; set; }
    }
}
