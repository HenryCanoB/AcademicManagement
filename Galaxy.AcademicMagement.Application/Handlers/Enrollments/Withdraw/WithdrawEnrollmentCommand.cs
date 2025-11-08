using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Enrollments.Withdraw
{
    public record WithdrawEnrollmentCommand(Guid EnrollmentId) : IRequest<bool>;
}
