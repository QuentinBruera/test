using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Context
{
    public class DirectoryDbContext(DbContextOptions<DirectoryDbContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Site> Site { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = "server=localhost;database=directory;user=root;password=";

            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
        }

    }
}
