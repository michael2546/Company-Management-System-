 using Demo.DAL.Data.Configrations;
using Demo.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //   => optionsBuilder.UseSqlServer("Server = . ; Database = MVCApp ; Trusted_Connection = True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Call Configuration Classes
            //modelBuilder.ApplyConfiguration<Department>(new DepartmentConfigrations());

            //this is better instead of add configration to each class indivedually
            //assembly referes to the current project classes

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
    
}
