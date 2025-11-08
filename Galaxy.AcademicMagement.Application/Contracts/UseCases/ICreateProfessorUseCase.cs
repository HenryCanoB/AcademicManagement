using Galaxy.AcademicMagement.Application.Dto.Professors;

namespace Galaxy.AcademicMagement.Application.Contracts.UseCases
{
    public interface ICreateProfessorUseCase
    {
        Task<Guid> ExecuteAsync(CreateProfessorRequest request);
    }
}
