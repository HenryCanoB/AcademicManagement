using Galaxy.AcademicMagement.Application.Contracts.Repositories;
using Galaxy.AcademicMagement.Domain.Entities;
using Galaxy.AcademicMagement.Infrastructure.Configurations.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Galaxy.AcademicMagement.Infrastructure.Persistence.Repositories
{
    public class ProfessorRepository : BaseRepository<Professor>, IProfessorRepository
    {
        public ProfessorRepository(AcademicManagementDbContext context) : base(context)
        {
        }

        public async Task<Professor?> GetByEmailAsync(string email)
        {
            return await _context.Professors
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<Professor?> GetByDocumentNumberAsync(string documentNumber)
        {
            return await _context.Professors
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Document.Number == documentNumber);
        }

        public async Task<ICollection<Professor>> GetProfessorsWithCoursesAsync()
        {
            return await _context.Professors
                .Include(p => p.Courses)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
