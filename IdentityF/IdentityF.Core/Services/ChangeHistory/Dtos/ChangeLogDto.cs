namespace IdentityF.Core.Services.ChangeHistory.Dtos
{
    public class ChangeLogDto
    {
        public int Id { get; set; }
        public DateTime ChangeDate { get; set; }
        public string ChangeType { get; set; }
        public string Data { get; set; }
        public int Version { get; set; }
    }
}
