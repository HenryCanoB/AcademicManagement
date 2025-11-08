using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Professors.Create
{
    public record CreateProfessorCommand(
        string FirstName,
        string LastName,
        string DocumentType,
        string DocumentNumber,
        string Email,
        string Specialization) : IRequest<Guid>;
}
