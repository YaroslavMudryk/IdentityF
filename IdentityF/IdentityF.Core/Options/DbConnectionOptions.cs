using IdentityF.Data.Enums;

namespace IdentityF.Core.Options
{
    public class DbConnectionOptions
    {
        public string ConnectionString { get; set; } = "Data Source=IdentityDb.db3";
        public DatabaseProviders DatabaseProvider { get; set; } = DatabaseProviders.Sqlite;
    }
}
