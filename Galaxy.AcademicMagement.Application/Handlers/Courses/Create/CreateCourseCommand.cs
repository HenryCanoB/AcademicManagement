using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Courses.Create
{
    public record CreateCourseCommand(
        string Name,
        string Description,
        int Credits,
        Guid ProfessorId) : IRequest<Guid>;
}
