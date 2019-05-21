using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;
using Polly;

namespace TodoListApi.DataAccess
{
    public class TodoAccessContext : DbContext
    {
        public TodoAccessContext(DbContextOptions<TodoAccessContext> options) : base(options)
        {
        }

        public DbSet<TodoModel> TodoModels { get; set; }

        //Table props
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoModel>().HasKey(i => i.Id);
            builder.Entity<TodoModel>().ToTable("TodoTable");
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
