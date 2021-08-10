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
            if (Users.Count() == 0)
            {
                Users.AddRange(new List<User>
                {
                    new User{ UserId=1, DateRegestration=DateTime.Now.AddDays(-3), DateLastVisit=DateTime.Now.AddDays(-1)},
                    new User{ UserId=2, DateRegestration=DateTime.Now.AddDays(-3), DateLastVisit=DateTime.Now.AddDays(-1)},
                    new User{ UserId=3, DateRegestration=DateTime.Now.AddDays(-3), DateLastVisit=DateTime.Now.AddDays(-1)},
                    new User{ UserId=4, DateRegestration=DateTime.Now.AddDays(-3), DateLastVisit=DateTime.Now.AddDays(-1)},
                    new User{ UserId=5, DateRegestration=DateTime.Now.AddDays(-3), DateLastVisit=DateTime.Now.AddDays(-1)},
                });
                SaveChanges();
            }
        }
    }
}
