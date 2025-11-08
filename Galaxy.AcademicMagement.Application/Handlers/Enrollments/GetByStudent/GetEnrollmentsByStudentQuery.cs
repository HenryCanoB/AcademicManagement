using Galaxy.AcademicMagement.Application.Dto.Enrollments;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Enrollments.GetByStudent
{
    public record GetEnrollmentsByStudentQuery(Guid StudentId) : IRequest<ICollection<EnrollmentResponse>>;
}
