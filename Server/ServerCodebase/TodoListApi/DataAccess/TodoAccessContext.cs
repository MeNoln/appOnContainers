using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoListApi.Models;

namespace TodoListApi.DataAccess
{
    public class TodoAccessContext : DbContext
    {
        public TodoAccessContext(DbContextOptions<TodoAccessContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<TodoModel> TodoModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<TodoModel>().HasKey(i => i.Id);
            builder.Entity<TodoModel>().ToTable("TodoTable");
            base.OnModelCreating(builder);
        }
    }
}
