namespace IdentityF.Data.Entities
{
    public class Contact : BaseModel<int>
    {
        public string Value { get; set; }
        public string Description { get; set; }
        public ContactType Type { get; set; }
        public ContactStatus Status { get; set; }
        public bool IsConfirmed { get; set; }
        public DateTime? ConfirmedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }

    public enum ContactType
    {
        Email,
        Phone
    }

    public enum ContactStatus
    {
        Main,
        Secondary
    }
}
