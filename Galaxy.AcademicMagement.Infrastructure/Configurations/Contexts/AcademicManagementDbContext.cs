using Galaxy.AcademicMagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.AcademicMagement.Infrastructure.Configurations.Contexts
{
    public class AcademicManagementDbContext : DbContext
    {
        public AcademicManagementDbContext() { }
        
        public AcademicManagementDbContext(DbContextOptions<AcademicManagementDbContext> options) : base(options)
        {
        }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Professor> Professors => Set<Professor>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();

        // SOLO PARA LA MIGRACIÓN A DB!! - Comentado por defecto
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        // Example configuration, replace with your actual connection string
        //        optionsBuilder.UseNpgsql("Host=localhost;Port=1601;Database=AcademicManagement_db;Username=admin;Password=password2025");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AcademicManagementDbContext).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }

       
    }
}