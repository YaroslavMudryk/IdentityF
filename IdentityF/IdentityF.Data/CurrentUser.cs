namespace IdentityF.Data;

public class CurrentUser
{
    public int Id { get; set; }
    public IEnumerable<string> Roles { get; set; }
}
