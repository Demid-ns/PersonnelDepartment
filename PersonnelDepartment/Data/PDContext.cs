using Microsoft.EntityFrameworkCore;
using PersonnelDepartment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelDepartment.Data
{
    public class PDContext : DbContext
    {
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Password> Passwords { get; set; }

        public PDContext(DbContextOptions<PDContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
