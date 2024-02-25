using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityF.Data.Entities
{
    public class BaseModel<TId> : BaseModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TId Id { get; set; }
    }

    public class BaseModel
    {
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedFromIp { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedFromIp { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string DeletedFromIp { get; set; }

        public int Version { get; set; }
        public string Signature { get; set; }
    }
}
