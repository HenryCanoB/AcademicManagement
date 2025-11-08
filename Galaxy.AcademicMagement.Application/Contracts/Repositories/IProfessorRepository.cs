using Galaxy.AcademicMagement.Domain.Entities;

namespace Galaxy.AcademicMagement.Application.Contracts.Repositories
{
    public interface IProfessorRepository : IBaseRepository<Professor>
    {
        Task<Professor?> GetByEmailAsync(string email);
        Task<Professor?> GetByDocumentNumberAsync(string documentNumber);
        Task<ICollection<Professor>> GetProfessorsWithCoursesAsync();
    }
}
