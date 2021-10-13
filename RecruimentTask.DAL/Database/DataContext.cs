using RecruimentTask.DAL.Models;
using System.Data.Entity;

namespace RecruimentTask.DAL.Database
{
    public class DataContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DataContext() : base("name=EmployeesDB")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasKey(x => x.EmployeeID);
        }
    }
}
