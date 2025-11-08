using Galaxy.AcademicMagement.Infrastructure.Configurations.Entities.IdentityContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.AcademicMagement.Infrastructure.Configurations.Contexts
{
    public class IdentityDbContext : IdentityDbContext<UserExtIdentity>
    {
        public IdentityDbContext() { }
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        //// solo para crear las tablas en base de datos
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=1601;Database=AcademicManagement_db;Username=admin;Password=password2025");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // cambiar el esquema a security
            builder.HasDefaultSchema("security");

            builder.Entity<UserExtIdentity>(entity =>
            {
                entity.ToTable(name: "User");
            });

            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
    }
}
