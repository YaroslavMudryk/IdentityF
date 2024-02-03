namespace IdentityF.Data.Entities
{
    public class App : BaseModel<int>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public bool IsActive { get; set; }
        public DateTime ActiveFrom { set; get; }
        public DateTime? ActiveTo { set; get; }
        public List<AppClaim> AppClaims { get; set; }

        public bool IsActiveByTime(DateTime dateTime)
        {
            return dateTime >= ActiveFrom && dateTime <= ActiveTo;
        }
    }
}
