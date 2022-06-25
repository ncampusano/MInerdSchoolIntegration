using Microsoft.EntityFrameworkCore;
using MinerdSchoolIntegration.Data.Entities;

namespace MInerdSchoolIntegration.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<School> School { get; set; }
        public DbSet<Grade> Grade { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Subject>().HasIndex(s => s.Name);
        }
    }
}