using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ReactTestTask.Models
{
    public class PostgreSQLContext: DbContext
    {
        public DbSet<User> Users { get; set; }       

        public PostgreSQLContext(DbContextOptions options) :base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=ReactDB;Username=postgres;Password=12345");
        }
    }
}
