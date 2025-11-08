using Galaxy.AcademicMagement.Domain.Entities;

namespace Galaxy.AcademicMagement.Application.Contracts.Repositories
{
    public interface IEnrollmentRepository : IBaseRepository<Enrollment>
    {
        Task<ICollection<Enrollment>> GetEnrollmentsByStudentAsync(Guid studentId);
        Task<ICollection<Enrollment>> GetEnrollmentsByCourseAsync(Guid courseId);
        Task<Enrollment?> GetEnrollmentByStudentAndCourseAsync(Guid studentId, Guid courseId);

        Task<ICollection<Enrollment>> GetAllWithRelationsAsync();

    }
}
