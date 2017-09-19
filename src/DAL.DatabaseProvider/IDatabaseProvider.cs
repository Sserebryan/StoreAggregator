using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL.DatabaseProvider
{
    public interface IDatabaseProvider
    {
        void SetOptions(IConfiguration configuration, DbContextOptionsBuilder dbContextOptionsBuilder);
    }
}
