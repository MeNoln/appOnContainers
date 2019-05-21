using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApi.Models;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace DiaryApi.DataAccess
{
    public class DiaryAccessContext : DbContext
    {
        public DiaryAccessContext(DbContextOptions<DiaryAccessContext> options) : base(options)
        {
        }

        public DbSet<DiaryModel> Diaries { get; set; }

        //Table props
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DiaryModel>().HasKey(k => k.Id);
            builder.Entity<DiaryModel>().ToTable("Diaries");
            base.OnModelCreating(builder);
        }

        //Method for auto migration(using Polly library)
        public void MigrateDB()
        {
            Policy
                .Handle<Exception>()
                .WaitAndRetry(10, r => TimeSpan.FromSeconds(10))
                .Execute(() => Database.Migrate());
        }
    }
}
