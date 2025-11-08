using Galaxy.AcademicMagement.Application.Dto.Enrollments;

namespace Galaxy.AcademicMagement.Application.Contracts.UseCases
{
    public interface ICreateEnrollmentUseCase
    {
        Task<Guid> ExecuteAsync(CreateEnrollmentRequest request);
    }
}
