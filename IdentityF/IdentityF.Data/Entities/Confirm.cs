namespace IdentityF.Data.Entities
{
    public class Confirm : BaseModel<int>
    {
        public string Code { get; set; }
        public DateTime ActiveFrom { get; set; }
        public DateTime ActiveTo { get; set; }
        public bool IsActivated { get; set; }
        public DateTime? ActivetedAt { get; set; }
        public ConfirmType Type { get; set; }
        public int? ContactId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public bool IsActualyRequest(DateTime dateTime)
        {
            return dateTime >= ActiveFrom && dateTime <= ActiveTo;
        }
    }
    public enum ConfirmType
    {
        Phone,
        Email
    }
}
