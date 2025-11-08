using Galaxy.AcademicMagement.Application.Dto.Courses;

namespace Galaxy.AcademicMagement.Application.Contracts.UseCases
{
    public interface ICreateCourseUseCase
    {
        Task<Guid> ExecuteAsync(CreateCourseRequest request);
    }
}
