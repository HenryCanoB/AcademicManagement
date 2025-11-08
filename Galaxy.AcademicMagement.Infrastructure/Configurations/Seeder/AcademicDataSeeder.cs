using Galaxy.AcademicMagement.Domain.Entities;
using Galaxy.AcademicMagement.Domain.ValueObjects;
using Galaxy.AcademicMagement.Infrastructure.Configurations.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.AcademicMagement.Infrastructure.Configurations.Seeder
{
    public static class AcademicDataSeeder
    {
        public static async Task SeedAsync(AcademicManagementDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (await context.Professors.AnyAsync())
            {
                return; 
            }

            var professors = new List<Professor>
            {
                new Professor(
                    "María", 
                    "González", 
                    new IdentityDocument("DNI", "12345678"),
                    "maria.gonzalez@universidad.edu", 
                    "Ciencias de la computación"
                ),
                new Professor(
                    "Carlos", 
                    "Rodríguez", 
                    new IdentityDocument("DNI", "87654321"),
                    "carlos.rodriguez@universidad.edu", 
                    "Matematica"
                ),
                new Professor(
                    "Ana", 
                    "López", 
                    new IdentityDocument("DNI", "11223344"), 
                    "ana.lopez@universidad.edu", 
                    "Fisica"
                )
            };

            await context.Professors.AddRangeAsync(professors);
            await context.SaveChangesAsync();

            // Seed Courses
            var courses = new List<Course>
            {
                new Course(
                    "Introducción a programación", 
                    "programación basica con C#", 
                    4, 
                    professors[0].Id
                ),
                new Course(
                    "Estructura de datos", 
                    "algoritmos", 
                    5, 
                    professors[0].Id
                ),
                new Course(
                    "Calculo I", 
                    "detalle", 
                    4, 
                    professors[1].Id
                ),
                new Course(
                    "Algebra lineal", 
                    "Matrices, Vectores, etc", 
                    3, 
                    professors[1].Id
                ),
                new Course(
                    "Fisica Basica", 
                    "detalle", 
                    4, 
                    professors[2].Id
                )
            };

            await context.Courses.AddRangeAsync(courses);
            await context.SaveChangesAsync();
        }
    }
}