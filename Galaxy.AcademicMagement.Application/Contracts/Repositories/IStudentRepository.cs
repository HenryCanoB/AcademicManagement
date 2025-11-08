using Galaxy.AcademicMagement.Domain.Entities;

namespace Galaxy.AcademicMagement.Application.Contracts.Repositories
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<Student?> GetByEmailAsync(string email);
        Task<ICollection<Student>> GetStudentsWithEnrollmentsAsync();
    }
}
