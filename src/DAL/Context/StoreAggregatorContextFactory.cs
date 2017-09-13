using DAL.DatabaseProvider;
using DAL.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;

namespace DAL.Context
{
    public class StoreAggregatorContextFactory : IDesignTimeDbContextFactory<StoreAggregatorContext>
    {
        public StoreAggregatorContextFactory()
        {
            _databaseProvider = new SqlServerProvider();
        }

        public StoreAggregatorContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<StoreAggregatorContext>();
            IConfigurationRoot configuration = new ConfigurationBuilder().AddUserSecrets("3CEE6F6C-C620-4D78-857E-6603B23564C5").Build();

            return new StoreAggregatorContext(builder.Options, configuration, _databaseProvider);
        }

        private IDatabaseProvider _databaseProvider;
    }
}