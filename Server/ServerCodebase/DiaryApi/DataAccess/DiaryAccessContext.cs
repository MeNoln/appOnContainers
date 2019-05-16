using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiaryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DiaryApi.DataAccess
{
    public class DiaryAccessContext : DbContext
    {
        public DiaryAccessContext(DbContextOptions<DiaryAccessContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<DiaryModel> Diaries { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<DiaryModel>().HasKey(k => k.Id);
            builder.Entity<DiaryModel>().ToTable("Diaries");
            base.OnModelCreating(builder);
        }
    }
}
