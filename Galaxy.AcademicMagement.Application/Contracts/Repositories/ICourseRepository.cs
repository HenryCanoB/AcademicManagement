using Galaxy.AcademicMagement.Domain.Entities;

namespace Galaxy.AcademicMagement.Application.Contracts.Repositories
{
    public interface ICourseRepository : IBaseRepository<Course>
    {
        Task<ICollection<Course>> GetCoursesByProfessorAsync(Guid professorId);
        Task<ICollection<Course>> GetAvailableCoursesAsync();
    }
}
