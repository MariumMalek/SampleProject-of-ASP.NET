using Microsoft.EntityFrameworkCore;
using SampleProject.Model;


namespace SampleProject.Data 
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        // DbSets for Employee and Department models
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        ////{
        ////    // Configuring the relationship between Employee and Department
        ////    modelBuilder.Entity<Employee>()
        ////        //.HasOne(e => e.Department)
        ////        .WithMany(d => d.Employees)
        ////        .HasForeignKey(e => e.DeptId);

        ////    base.OnModelCreating(modelBuilder);
        //}
    }
}
