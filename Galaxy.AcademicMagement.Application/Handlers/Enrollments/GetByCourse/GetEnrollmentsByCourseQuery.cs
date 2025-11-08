using Galaxy.AcademicMagement.Application.Dto.Enrollments;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Enrollments.GetByCourse
{
    public record GetEnrollmentsByCourseQuery(Guid CourseId) : IRequest<ICollection<EnrollmentResponse>>;
}
