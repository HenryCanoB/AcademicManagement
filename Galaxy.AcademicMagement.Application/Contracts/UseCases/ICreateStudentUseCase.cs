using Galaxy.AcademicMagement.Application.Dto.Students;

namespace Galaxy.AcademicMagement.Application.Contracts.UseCases
{
    public interface ICreateStudentUseCase
    {
        Task<Guid> ExecuteAsync(CreateStudentRequest request);
    }
}
