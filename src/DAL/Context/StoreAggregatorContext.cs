using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using DAL.DatabaseProvider;
using DAL.Models;
using DAL.Models.IdentityModels;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            modelBuilder.ApplyConfiguration<Tag>(new TagMap());

            modelBuilder.Entity<Tag>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Tag>()
                .Property(b => b.Name)
                .HasMaxLength(16)
                .IsRequired();
        }

        private IConfiguration _configuration { get; set; }

        private IDatabaseProvider _databaseProvider { get; set; }

        private class TagMap : IEntityTypeConfiguration<Tag>
        {
            public void Configure(EntityTypeBuilder<Tag> builder)
            {
                builder.HasKey(c => c.Id);
            }
        }

    }
}