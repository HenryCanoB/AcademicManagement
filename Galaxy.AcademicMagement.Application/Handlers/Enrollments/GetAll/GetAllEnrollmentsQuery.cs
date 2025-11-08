using Galaxy.AcademicMagement.Application.Dto.Enrollments;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Enrollments.GetAll
{
    public record GetAllEnrollmentsQuery() : IRequest<ICollection<EnrollmentResponse>>;
}
