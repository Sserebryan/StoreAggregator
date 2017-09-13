using System;
using DAL.Models;
using DAL.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DAL.DatabaseProvider;

namespace DAL.Context
{
    public class StoreAggregatorContext: IdentityDbContext<User, Role, Int32> 
    {
        public StoreAggregatorContext(DbContextOptions<StoreAggregatorContext> options, IConfiguration configuration, IDatabaseProvider databaseProvider)
            : base(options)
        {
            _configuration = configuration;
            _databaseProvider = databaseProvider;
        }
        
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            _databaseProvider.SetOptions(_configuration, dbContextOptionsBuilder);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Tag>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Tag>()
                .Property(b => b.Name)
                .IsRequired();
        }

        private IConfiguration _configuration { get; set; }

        private IDatabaseProvider _databaseProvider { get; set; }
        
    }
}