using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace ReactTestTask.Models
{
    public class PostgreSQLContext: DbContext
    {
        public DbSet<User> Users { get; set; }       

        public PostgreSQLContext(DbContextOptions options) :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
