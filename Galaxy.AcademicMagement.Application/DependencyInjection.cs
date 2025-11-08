using Galaxy.AcademicMagement.Application.Contracts.UseCases;
using Galaxy.AcademicMagement.Application.UseCase.Courses;
using Galaxy.AcademicMagement.Application.UseCase.Enrollments;
using Galaxy.AcademicMagement.Application.UseCase.Professors;
using Galaxy.AcademicMagement.Application.UseCase.Students;
using Microsoft.Extensions.DependencyInjection;

namespace Galaxy.AcademicMagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register Use Cases
            services.AddScoped<ICreateStudentUseCase, CreateStudentUseCase>();
            services.AddScoped<ICreateProfessorUseCase, CreateProfessorUseCase>();
            services.AddScoped<ICreateCourseUseCase, CreateCourseUseCase>();
            services.AddScoped<ICreateEnrollmentUseCase, CreateEnrollmentUseCase>();

            return services;
        }
    }
}
