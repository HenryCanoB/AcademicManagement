using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Enrollments.Create
{
    public record CreateEnrollmentCommand(
        Guid StudentId, 
        Guid CourseId) : IRequest<Guid>;
}
