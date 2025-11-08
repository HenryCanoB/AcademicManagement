using Galaxy.AcademicMagement.Application.Dto.Professors;
using MediatR;

namespace Galaxy.AcademicMagement.Application.Handlers.Professors.GetAll
{
    public record GetAllProfessorsQuery() : IRequest<ICollection<ProfessorResponse>>;
}
