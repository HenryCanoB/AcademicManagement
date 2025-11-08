using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Application.Contracts.Services;
using Galaxy.AcademicMagement.Infrastructure.Configurations.Contexts;
using Galaxy.AcademicMagement.Infrastructure.Configurations.Entities.IdentityContext;
using Galaxy.AcademicMagement.Infrastructure.Persistence.Repositories;
using Galaxy.AcademicMagement.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.AcademicMagement.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Add DbContext
            services.AddDbContext<AcademicManagementDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("AcademicManagementDb");
                options.UseNpgsql(connectionString);
            });

            services.AddDbContext<IdentityDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("AcademicManagementDb"));
            });

            //ASPNETCore IDENTITY Policy Injection
            services.AddIdentity<UserExtIdentity, IdentityRole>(policy =>
            {
                policy.Password.RequiredLength = 6; // longitud minima
                policy.Password.RequireDigit = true; // requiere numeros
                policy.Password.RequireUppercase = true; // requiere mayusculas
                policy.Password.RequireLowercase = true; // requiere minusculas
                policy.Password.RequireNonAlphanumeric = false; // no requiere caracteres especiales

                policy.User.RequireUniqueEmail = true; // email unico por usuario

                policy.Lockout.MaxFailedAccessAttempts = 5; // numero de intentos fallidos
                policy.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // tiempo de bloqueo

                policy.Lockout.AllowedForNewUsers = true; // permitir bloqueo para nuevos usuarios

            })
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            // Register Repositories
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            // Register Services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAcademicManagementUnitOfWork, AcademicManagementUnitOfWork>();

            return services;
        }
    }
}