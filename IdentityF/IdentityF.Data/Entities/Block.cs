using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityF.Data.Entities
{
    public class Block : BaseModel<int>
    {
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public string Cause { get; set; }
        [NotMapped]
        public bool IsPermanent => Finish == DateTime.MaxValue;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
