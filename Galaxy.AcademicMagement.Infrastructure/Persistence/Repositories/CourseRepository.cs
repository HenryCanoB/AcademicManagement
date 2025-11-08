using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Domain.Entities;
using Galaxy.AcademicMagement.Infrastructure.Configurations.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.AcademicMagement.Infrastructure.Persistence.Repositories
{
    public class CourseRepository : BaseRepository<Course>, ICourseRepository
    {
        public CourseRepository(AcademicManagementDbContext context) : base(context)
        {
        }

        public async Task<ICollection<Course>> GetCoursesByProfessorAsync(Guid professorId)
        {
            return await _context.Courses
                .Where(c => c.ProfessorId == professorId && c.IsActive)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ICollection<Course>> GetAvailableCoursesAsync()
        {
            return await _context.Courses
                .Where(c => c.IsActive)
                .Include(c => c.Professor)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
