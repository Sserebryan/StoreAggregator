using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DAL.DatabaseProvider;

namespace DAL.SqlServer
{
    public class SqlServerProvider : IDatabaseProvider
    {
        public void SetOptions(IConfiguration configuration, DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            dbContextOptionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), x => x.MigrationsAssembly("DAL"));
        }
    }
}
