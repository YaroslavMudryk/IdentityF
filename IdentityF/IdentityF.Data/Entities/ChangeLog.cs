using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityF.Data.Entities
{
    public class ChangeLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string EntityName { get; set; }
        public string EntitySignature { get; set; }
        public string EntityId { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangeType { get; set; } // Insert, Update, Delete
        public string Data { get; set; }
        public int Version { get; set; }
    }

    public enum ChangeType
    {
        Insert = 1,
        Update = 2
    }
}
